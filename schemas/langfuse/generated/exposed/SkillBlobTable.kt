package langfuse.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object SkillBlobTable : IdTable<String>("skill_blobs") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val projectId =
        reference(
            "project_id",
            ProjectTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "skill_blobs_project_id_fkey",
        )
    val sha256Hash = char("sha_256_hash", 44)
    val contentType = text("content_type")
    val contentLength = integer("content_length")
    val content = text("content")
    override val primaryKey = PrimaryKey(id, name = "skill_blobs_pkey")

    init {
        uniqueIndex("skill_blobs_project_id_sha_256_hash_key", projectId, sha256Hash)
    }
}
