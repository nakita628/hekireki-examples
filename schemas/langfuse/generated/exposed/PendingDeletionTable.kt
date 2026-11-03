package langfuse.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object PendingDeletionTable : IdTable<String>("pending_deletions") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val projectId =
        reference(
            "project_id",
            ProjectTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "pending_deletions_project_id_fkey",
        )
    val `object` = text("object")
    val objectId = text("object_id")
    val isDeleted = bool("is_deleted").default(false)
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "pending_deletions_pkey")

    init {
        index("pending_deletions_project_id_object_is_deleted_object_id_id_idx", false, projectId, `object`, isDeleted, objectId, id)
        index("pending_deletions_object_id_object_idx", false, objectId, `object`)
    }
}
