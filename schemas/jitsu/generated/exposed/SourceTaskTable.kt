package jitsu.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object SourceTaskTable : IdTable<String>("source_task") {
    val syncId = text("sync_id")
    override val id = text("task_id").entityId()
    val `package` = text("package")
    val version = text("version")
    val startedAt = pgTimestamptz("started_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamptz("updated_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val startedBy = jsonb<String>("started_by", { it }, { it }).nullable()
    val status = text("status")
    val description = text("description").nullable()
    val metrics = jsonb<String>("metrics", { it }, { it }).nullable()
    val error = text("error").nullable()
    override val primaryKey = PrimaryKey(id, name = "source_task_pkey")

    init {
        index("source_task_sync_id_idx", false, syncId)
        index("source_task_started_at_idx", false, startedAt)
        index(
            "source_task_sync_id_started_at_idx",
            false,
            functions = listOf(indexExpression("\"sync_id\""), indexExpression("\"started_at\" DESC")),
        )
    }
}
