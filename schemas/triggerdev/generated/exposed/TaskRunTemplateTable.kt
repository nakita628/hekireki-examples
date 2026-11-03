package triggerdev.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object TaskRunTemplateTable : IdTable<String>("\"TaskRunTemplate\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val taskSlug = text("taskSlug")
    val triggerSource = pgEnum("triggerSource", "\"TaskTriggerSource\"", TaskTriggerSource.entries, TaskTriggerSource::dbName)
    val label = text("label")
    val payload = text("payload").nullable()
    val payloadType = text("payloadType").default("application/json")
    val metadata = text("metadata").nullable()
    val metadataType = text("metadataType").default("application/json")
    val queue = text("queue").nullable()
    val concurrencyKey = text("concurrencyKey").nullable()
    val ttlSeconds = integer("ttlSeconds").nullable()
    val maxAttempts = integer("maxAttempts").nullable()
    val maxDurationSeconds = integer("maxDurationSeconds").nullable()
    val tags = array("tags", TextColumnType()).nullable()
    val machinePreset = text("machinePreset").nullable()
    val projectId = text("projectId")
    val organizationId = text("organizationId")
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"TaskRunTemplate_pkey\"")

    init {
        index(
            "TaskRunTemplate_projectId_taskSlug_triggerSource_createdAt_idx",
            false,
            functions =
                listOf(
                    indexExpression("\"projectId\""),
                    indexExpression("\"taskSlug\""),
                    indexExpression("\"triggerSource\""),
                    indexExpression("\"createdAt\" DESC"),
                ),
        )
    }
}
