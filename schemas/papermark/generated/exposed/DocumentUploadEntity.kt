package papermark.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class DocumentUploadEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, DocumentUploadEntity>(DocumentUploadTable)

    var documentId by DocumentUploadTable.documentId
    var document by DocumentEntity referencedOn DocumentUploadTable.documentId
    var teamId by DocumentUploadTable.teamId
    var team by TeamEntity referencedOn DocumentUploadTable.teamId
    var viewerId by DocumentUploadTable.viewerId
    var viewer by ViewerEntity optionalReferencedOn DocumentUploadTable.viewerId
    var viewId by DocumentUploadTable.viewId
    var view by ViewEntity optionalReferencedOn DocumentUploadTable.viewId
    var linkId by DocumentUploadTable.linkId
    var link by LinkEntity referencedOn DocumentUploadTable.linkId
    var dataroomId by DocumentUploadTable.dataroomId
    var dataroom by DataroomEntity optionalReferencedOn DocumentUploadTable.dataroomId
    var dataroomDocumentId by DocumentUploadTable.dataroomDocumentId
    var dataroomDocument by DataroomDocumentEntity optionalReferencedOn DocumentUploadTable.dataroomDocumentId
    var taskId by DocumentUploadTable.taskId
    var task by TaskEntity optionalReferencedOn DocumentUploadTable.taskId
    var originalFilename by DocumentUploadTable.originalFilename
    var fileSize by DocumentUploadTable.fileSize
    var numPages by DocumentUploadTable.numPages
    var mimeType by DocumentUploadTable.mimeType
    var uploadedAt by DocumentUploadTable.uploadedAt
    var createdAt by DocumentUploadTable.createdAt
    var updatedAt by DocumentUploadTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(DocumentUploadTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
