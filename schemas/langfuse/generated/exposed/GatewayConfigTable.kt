package langfuse.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object GatewayConfigTable : IdTable<String>("gateway_configs") {
    override val id =
        text("organization_id")
            .references(
                OrganizationTable.id,
                onDelete = ReferenceOption.CASCADE,
                onUpdate = ReferenceOption.CASCADE,
                fkName = "gateway_configs_organization_id_fkey",
            ).entityId()
    val defaultIngestionProjectId =
        optReference(
            "default_ingestion_project_id",
            ProjectTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "gateway_configs_default_ingestion_project_id_fkey",
        )
    val ingestionMode =
        pgEnum("ingestion_mode", "\"GatewayIngestionMode\"", GatewayIngestionMode.entries, GatewayIngestionMode::dbName).default(
            GatewayIngestionMode.USAGE,
        )
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "gateway_configs_pkey")
}
