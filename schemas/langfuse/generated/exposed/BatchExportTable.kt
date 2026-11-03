package langfuse.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object BatchExportTable : IdTable<String>("batch_exports") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val projectId =
        reference(
            "project_id",
            ProjectTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "batch_exports_project_id_fkey",
        )
    val userId = text("user_id")
    val finishedAt = pgTimestamp("finished_at", 3).nullable()
    val expiresAt = pgTimestamp("expires_at", 3).nullable()
    val name = text("name")
    val status = text("status")
    val query = jsonb<String>("query", { it }, { it })
    val format = text("format")
    val url = text("url").nullable()
    val log = text("log").nullable()
    override val primaryKey = PrimaryKey(id, name = "batch_exports_pkey")

    init {
        index("batch_exports_project_id_user_id_idx", false, projectId, userId)
        index("batch_exports_status_idx", false, status)
    }
}
