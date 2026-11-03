package papermark.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class ConversationEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ConversationEntity>(ConversationTable)

    var title by ConversationTable.title
    var isEnabled by ConversationTable.isEnabled
    var visibilityMode by ConversationTable.visibilityMode
    var dataroomId by ConversationTable.dataroomId
    var dataroom by DataroomEntity referencedOn ConversationTable.dataroomId
    var dataroomDocumentId by ConversationTable.dataroomDocumentId
    var dataroomDocument by DataroomDocumentEntity optionalReferencedOn ConversationTable.dataroomDocumentId
    var documentVersionNumber by ConversationTable.documentVersionNumber
    var documentPageNumber by ConversationTable.documentPageNumber
    var linkId by ConversationTable.linkId
    var link by LinkEntity optionalReferencedOn ConversationTable.linkId
    var viewerGroupId by ConversationTable.viewerGroupId
    var viewerGroup by ViewerGroupEntity optionalReferencedOn ConversationTable.viewerGroupId
    var questionId by ConversationTable.questionId
    var question by DataroomQuestionEntity optionalReferencedOn ConversationTable.questionId
    var origin by ConversationTable.origin
    var category by ConversationTable.category
    var priority by ConversationTable.priority
    var recipientViewerId by ConversationTable.recipientViewerId
    var recipientViewer by ViewerEntity optionalReferencedOn ConversationTable.recipientViewerId
    var initialViewId by ConversationTable.initialViewId
    var initialView by ViewEntity optionalReferencedOn ConversationTable.initialViewId
    var teamId by ConversationTable.teamId
    var team by TeamEntity referencedOn ConversationTable.teamId
    val views by ConversationViewEntity referrersOn ConversationViewTable.conversationId
    val participants by ConversationParticipantEntity referrersOn ConversationParticipantTable.conversationId
    val messages by MessageEntity referrersOn MessageTable.conversationId
    var lastMessageAt by ConversationTable.lastMessageAt
    val faqItems by DataroomFaqItemEntity optionalReferrersOn DataroomFaqItemTable.sourceConversationId
    var createdAt by ConversationTable.createdAt
    var updatedAt by ConversationTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ConversationTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
