package langfuse.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object SkillFileTable : IdTable<String>("skill_files") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val projectId =
        reference(
            "project_id",
            ProjectTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "skill_files_project_id_fkey",
        )
    val skillId =
        reference(
            "skill_id",
            SkillTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "skill_files_skill_id_fkey",
        )
    val blobId =
        reference(
            "blob_id",
            SkillBlobTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "skill_files_blob_id_fkey",
        )
    val path = text("path")
    override val primaryKey = PrimaryKey(id, name = "skill_files_pkey")

    init {
        index("skill_files_project_id_blob_id_idx", false, projectId, blobId)
        uniqueIndex("skill_files_project_id_skill_id_path_key", projectId, skillId, path)
    }
}
