package papermark.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class DataroomFaqItemEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, DataroomFaqItemEntity>(DataroomFaqItemTable)

    var title by DataroomFaqItemTable.title
    var editedQuestion by DataroomFaqItemTable.editedQuestion
    var originalQuestion by DataroomFaqItemTable.originalQuestion
    var answer by DataroomFaqItemTable.answer
    var description by DataroomFaqItemTable.description
    var dataroomId by DataroomFaqItemTable.dataroomId
    var dataroom by DataroomEntity referencedOn DataroomFaqItemTable.dataroomId
    var linkId by DataroomFaqItemTable.linkId
    var link by LinkEntity optionalReferencedOn DataroomFaqItemTable.linkId
    var dataroomDocumentId by DataroomFaqItemTable.dataroomDocumentId
    var dataroomDocument by DataroomDocumentEntity optionalReferencedOn DataroomFaqItemTable.dataroomDocumentId
    var sourceConversationId by DataroomFaqItemTable.sourceConversationId
    var sourceConversation by ConversationEntity optionalReferencedOn DataroomFaqItemTable.sourceConversationId
    var questionMessageId by DataroomFaqItemTable.questionMessageId
    var questionMessage by MessageEntity optionalReferencedOn DataroomFaqItemTable.questionMessageId
    var answerMessageId by DataroomFaqItemTable.answerMessageId
    var answerMessage by MessageEntity optionalReferencedOn DataroomFaqItemTable.answerMessageId
    var teamId by DataroomFaqItemTable.teamId
    var team by TeamEntity referencedOn DataroomFaqItemTable.teamId
    var publishedByUserId by DataroomFaqItemTable.publishedByUserId
    var publishedByUser by UserEntity referencedOn DataroomFaqItemTable.publishedByUserId
    var visibilityMode by DataroomFaqItemTable.visibilityMode
    var status by DataroomFaqItemTable.status
    var isAnonymized by DataroomFaqItemTable.isAnonymized
    var viewCount by DataroomFaqItemTable.viewCount
    var tags by DataroomFaqItemTable.tags
    var documentPageNumber by DataroomFaqItemTable.documentPageNumber
    var documentVersionNumber by DataroomFaqItemTable.documentVersionNumber
    var createdAt by DataroomFaqItemTable.createdAt
    var updatedAt by DataroomFaqItemTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(DataroomFaqItemTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
