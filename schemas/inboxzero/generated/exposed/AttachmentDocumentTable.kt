package inboxzero.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object AttachmentDocumentTable : IdTable<String>("\"AttachmentDocument\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val attachmentSourceId =
        reference(
            "attachmentSourceId",
            AttachmentSourceTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"AttachmentDocument_attachmentSourceId_fkey\"",
        )
    val fileId = text("fileId")
    val name = text("name")
    val mimeType = text("mimeType")
    val modifiedAt = pgTimestamp("modifiedAt", 3).nullable()
    val summary = text("summary").nullable()
    val content = text("content").nullable()
    val metadata = jsonb<String>("metadata", { it }, { it }).nullable()
    val indexedAt = pgTimestamp("indexedAt", 3).nullable()
    val error = text("error").nullable()
    override val primaryKey = PrimaryKey(id, name = "\"AttachmentDocument_pkey\"")

    init {
        index("\"AttachmentDocument_attachmentSourceId_modifiedAt_idx\"", false, attachmentSourceId, modifiedAt)
        uniqueIndex("\"AttachmentDocument_attachmentSourceId_fileId_key\"", attachmentSourceId, fileId)
    }
}
