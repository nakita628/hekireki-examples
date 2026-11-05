package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class ConversationActivityEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ConversationActivityEntity>(ConversationActivityTable)

    var participantId by ConversationActivityTable.participantId
    var participant by ConversationParticipantEntity referencedOn ConversationActivityTable.participantId
    var commentId by ConversationActivityTable.commentId
    var comment by ConversationCommentEntity optionalReferencedOn ConversationActivityTable.commentId
    var kind by ConversationActivityTable.kind
    var revision by ConversationActivityTable.revision
    var createdAt by ConversationActivityTable.createdAt
}
