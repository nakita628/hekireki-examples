package papermark.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class PendingDocumentUploadEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, PendingDocumentUploadEntity>(PendingDocumentUploadTable)

    var uploadId by PendingDocumentUploadTable.uploadId
    var teamId by PendingDocumentUploadTable.teamId
    var tokenId by PendingDocumentUploadTable.tokenId
    var key by PendingDocumentUploadTable.key
    var contentType by PendingDocumentUploadTable.contentType
    var storageType by PendingDocumentUploadTable.storageType
    var expiresAt by PendingDocumentUploadTable.expiresAt
    var consumedAt by PendingDocumentUploadTable.consumedAt
    var createdAt by PendingDocumentUploadTable.createdAt
}
