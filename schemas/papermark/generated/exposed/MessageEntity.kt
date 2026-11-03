package papermark.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class MessageEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, MessageEntity>(MessageTable)

    var content by MessageTable.content
    var conversationId by MessageTable.conversationId
    var conversation by ConversationEntity referencedOn MessageTable.conversationId
    var userId by MessageTable.userId
    var user by UserEntity optionalReferencedOn MessageTable.userId
    var viewerId by MessageTable.viewerId
    var viewer by ViewerEntity optionalReferencedOn MessageTable.viewerId
    var viewId by MessageTable.viewId
    var view by ViewEntity optionalReferencedOn MessageTable.viewId
    var isRead by MessageTable.isRead
    var isInternalNote by MessageTable.isInternalNote
    val faqAsQuestion by DataroomFaqItemEntity optionalReferrersOn DataroomFaqItemTable.questionMessageId
    val faqAsAnswer by DataroomFaqItemEntity optionalReferrersOn DataroomFaqItemTable.answerMessageId
    var createdAt by MessageTable.createdAt
    var updatedAt by MessageTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(MessageTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
