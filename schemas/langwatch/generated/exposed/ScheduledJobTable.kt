package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object ScheduledJobTable : IdTable<String>("\"ScheduledJob\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val projectId = text("projectId")
    val targetType = text("targetType")
    val targetId = text("targetId")
    val cron = text("cron")
    val timezone = text("timezone")
    val nextRunAt = pgTimestamp("nextRunAt", 3)
    val lastSlot = pgTimestamp("lastSlot", 3).nullable()
    val currentSlot = pgTimestamp("currentSlot", 3).nullable()
    val attempts = integer("attempts").default(0)
    val lastError = text("lastError").nullable()
    val active = bool("active").default(true)
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"ScheduledJob_pkey\"")

    init {
        index("\"ScheduledJob_active_nextRunAt_idx\"", false, active, nextRunAt)
        index("\"ScheduledJob_projectId_idx\"", false, projectId)
        uniqueIndex("\"ScheduledJob_targetType_targetId_key\"", targetType, targetId)
    }
}
