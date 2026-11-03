package triggerdev.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object TaskScheduleTable : IdTable<String>("\"TaskSchedule\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val type = pgEnum("type", "\"ScheduleType\"", ScheduleType.entries, ScheduleType::dbName).default(ScheduleType.IMPERATIVE)
    val friendlyId = text("friendlyId").uniqueIndex("\"TaskSchedule_friendlyId_key\"")
    val taskIdentifier = text("taskIdentifier")
    val deduplicationKey = text("deduplicationKey").clientDefault { CUID.randomCUID1().toString() }
    val userProvidedDeduplicationKey = bool("userProvidedDeduplicationKey").default(false)
    val generatorExpression = text("generatorExpression")
    val generatorDescription = text("generatorDescription").default("")
    val generatorType =
        pgEnum("generatorType", "\"ScheduleGeneratorType\"", ScheduleGeneratorType.entries, ScheduleGeneratorType::dbName).default(
            ScheduleGeneratorType.CRON,
        )
    val timezone = text("timezone").default("UTC")
    val windowDurationSeconds = integer("windowDurationSeconds").nullable()
    val windowPercentage = integer("windowPercentage").nullable()
    val defaultWindowDurationSeconds = integer("defaultWindowDurationSeconds").nullable()
    val minimumWindowDurationSeconds = integer("minimumWindowDurationSeconds").nullable()
    val externalId = text("externalId").nullable()
    val lastRunTriggeredAt = pgTimestamp("lastRunTriggeredAt", 3).nullable()
    val projectId =
        reference(
            "projectId",
            ProjectTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"TaskSchedule_projectId_fkey\"",
        )
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val active = bool("active").default(true)
    override val primaryKey = PrimaryKey(id, name = "\"TaskSchedule_pkey\"")

    init {
        index("\"TaskSchedule_projectId_idx\"", false, projectId)
        index(
            "\"TaskSchedule_projectId_createdAt_idx\"",
            false,
            functions = listOf(indexExpression("\"projectId\""), indexExpression("\"createdAt\" DESC")),
        )
        uniqueIndex("\"TaskSchedule_projectId_deduplicationKey_key\"", projectId, deduplicationKey)
    }
}
