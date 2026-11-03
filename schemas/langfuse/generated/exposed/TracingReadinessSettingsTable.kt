package langfuse.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object TracingReadinessSettingsTable : IdTable<String>("tracing_readiness_settings") {
    override val id =
        text("project_id")
            .references(
                ProjectTable.id,
                onDelete = ReferenceOption.CASCADE,
                onUpdate = ReferenceOption.CASCADE,
                fkName = "tracing_readiness_settings_project_id_fkey",
            ).entityId()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val createdBy =
        optReference(
            "created_by",
            UserTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "tracing_readiness_settings_created_by_fkey",
        )
    val updatedBy =
        optReference(
            "updated_by",
            UserTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "tracing_readiness_settings_updated_by_fkey",
        )
    val traceIdleMs = integer("trace_idle_ms").nullable()
    override val primaryKey = PrimaryKey(id, name = "tracing_readiness_settings_pkey")
}
