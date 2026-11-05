package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class AttachmentDocumentEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, AttachmentDocumentEntity>(AttachmentDocumentTable)

    var createdAt by AttachmentDocumentTable.createdAt
    var updatedAt by AttachmentDocumentTable.updatedAt
    var attachmentSourceId by AttachmentDocumentTable.attachmentSourceId
    var attachmentSource by AttachmentSourceEntity referencedOn AttachmentDocumentTable.attachmentSourceId
    var fileId by AttachmentDocumentTable.fileId
    var name by AttachmentDocumentTable.name
    var mimeType by AttachmentDocumentTable.mimeType
    var modifiedAt by AttachmentDocumentTable.modifiedAt
    var summary by AttachmentDocumentTable.summary
    var content by AttachmentDocumentTable.content
    var metadata by AttachmentDocumentTable.metadata
    var indexedAt by AttachmentDocumentTable.indexedAt
    var error by AttachmentDocumentTable.error

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(AttachmentDocumentTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
