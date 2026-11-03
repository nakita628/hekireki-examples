package papermark.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class DocumentEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, DocumentEntity>(DocumentTable)

    var name by DocumentTable.name
    var description by DocumentTable.description
    var file by DocumentTable.file
    var originalFile by DocumentTable.originalFile
    var type by DocumentTable.type
    var contentType by DocumentTable.contentType
    var storageType by DocumentTable.storageType
    var numPages by DocumentTable.numPages
    var owner by UserEntity optionalReferencedOn DocumentTable.ownerId
    var teamId by DocumentTable.teamId
    var team by TeamEntity referencedOn DocumentTable.teamId
    var ownerId by DocumentTable.ownerId
    var assistantEnabled by DocumentTable.assistantEnabled
    var advancedExcelEnabled by DocumentTable.advancedExcelEnabled
    var agentsEnabled by DocumentTable.agentsEnabled
    var downloadOnly by DocumentTable.downloadOnly
    var hiddenInAllDocuments by DocumentTable.hiddenInAllDocuments
    var createdAt by DocumentTable.createdAt
    var updatedAt by DocumentTable.updatedAt
    val links by LinkEntity optionalReferrersOn LinkTable.documentId
    val views by ViewEntity optionalReferrersOn ViewTable.documentId
    val versions by DocumentVersionEntity referrersOn DocumentVersionTable.documentId
    val chats by ChatEntity optionalReferrersOn ChatTable.documentId
    var folderId by DocumentTable.folderId
    var folder by FolderEntity optionalReferencedOn DocumentTable.folderId
    val datarooms by DataroomDocumentEntity referrersOn DataroomDocumentTable.documentId
    val tags by TagItemEntity optionalReferrersOn TagItemTable.documentId
    val annotations by DocumentAnnotationEntity referrersOn DocumentAnnotationTable.documentId
    val redactionJobs by DocumentRedactionJobEntity referrersOn DocumentRedactionJobTable.documentId
    val uploadedDocument by DocumentUploadEntity referrersOn DocumentUploadTable.documentId
    var isExternalUpload by DocumentTable.isExternalUpload

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(DocumentTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
