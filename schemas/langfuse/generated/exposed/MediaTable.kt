package langfuse.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object MediaTable : CompositeIdTable("media") {
    val id1 = text("id").entityId()
    val sha256Hash = char("sha_256_hash", 44)
    val projectId =
        reference(
            "project_id",
            ProjectTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "media_project_id_fkey",
        )
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val uploadedAt = pgTimestamp("uploaded_at", 3).nullable()
    val uploadHttpStatus = integer("upload_http_status").nullable()
    val uploadHttpError = text("upload_http_error").nullable()
    val bucketPath = text("bucket_path")
    val bucketName = text("bucket_name")
    val contentType = text("content_type")
    val contentLength = long("content_length")

    init {
        addIdColumn(projectId)
        index("media_project_id_created_at_idx", false, projectId, createdAt)
        uniqueIndex("media_project_id_id_key", projectId, id1)
        uniqueIndex("media_project_id_sha_256_hash_key", projectId, sha256Hash)
    }
}
