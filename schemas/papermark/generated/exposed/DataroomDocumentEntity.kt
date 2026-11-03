package papermark.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class DataroomDocumentEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, DataroomDocumentEntity>(DataroomDocumentTable)

    var dataroomId by DataroomDocumentTable.dataroomId
    var dataroom by DataroomEntity referencedOn DataroomDocumentTable.dataroomId
    var documentId by DataroomDocumentTable.documentId
    var document by DocumentEntity referencedOn DataroomDocumentTable.documentId
    var folderId by DataroomDocumentTable.folderId
    var folder by DataroomFolderEntity optionalReferencedOn DataroomDocumentTable.folderId
    var orderIndex by DataroomDocumentTable.orderIndex
    var hierarchicalIndex by DataroomDocumentTable.hierarchicalIndex
    val conversations by ConversationEntity optionalReferrersOn ConversationTable.dataroomDocumentId
    val questions by DataroomQuestionEntity optionalReferrersOn DataroomQuestionTable.dataroomDocumentId
    val dataroomFAQs by DataroomFaqItemEntity optionalReferrersOn DataroomFaqItemTable.dataroomDocumentId
    val uploadedDocuments by DocumentUploadEntity optionalReferrersOn DocumentUploadTable.dataroomDocumentId
    var vectorStoreFileId by DataroomDocumentTable.vectorStoreFileId
    var createdAt by DataroomDocumentTable.createdAt
    var updatedAt by DataroomDocumentTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(DataroomDocumentTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
