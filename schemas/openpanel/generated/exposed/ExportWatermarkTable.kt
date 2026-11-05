package openpanel.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.core.java.javaUUID
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object ExportWatermarkTable : IdTable<UUID>("export_watermarks") {
    override val id = javaUUID("id").databaseDefault("gen_random_uuid()").entityId()
    val projectId =
        reference(
            "projectId",
            ProjectTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"export_watermarks_projectId_fkey\"",
        )
    val integrationId =
        reference(
            "integrationId",
            IntegrationTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"export_watermarks_integrationId_fkey\"",
        )
    val lastInsertedAt = pgTimestamp("lastInsertedAt", 3)
    val lastEventId = text("lastEventId").default("")
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "export_watermarks_pkey")

    init {
        index("\"export_watermarks_integrationId_idx\"", false, integrationId)
        uniqueIndex("\"export_watermarks_projectId_integrationId_key\"", projectId, integrationId)
    }
}
