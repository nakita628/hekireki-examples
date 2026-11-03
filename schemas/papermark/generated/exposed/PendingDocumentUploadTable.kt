package papermark.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object PendingDocumentUploadTable : IdTable<String>("\"PendingDocumentUpload\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val uploadId = text("uploadId").uniqueIndex("\"PendingDocumentUpload_uploadId_key\"")
    val teamId = text("teamId")
    val tokenId = text("tokenId")
    val key = text("key")
    val contentType = text("contentType")
    val storageType =
        pgEnum("storageType", "\"DocumentStorageType\"", DocumentStorageType.entries, DocumentStorageType::dbName).default(
            DocumentStorageType.S3_PATH,
        )
    val expiresAt = pgTimestamp("expiresAt", 3)
    val consumedAt = pgTimestamp("consumedAt", 3).nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"PendingDocumentUpload_pkey\"")

    init {
        index("\"PendingDocumentUpload_teamId_expiresAt_idx\"", false, teamId, expiresAt)
        index("\"PendingDocumentUpload_tokenId_expiresAt_idx\"", false, tokenId, expiresAt)
    }
}
