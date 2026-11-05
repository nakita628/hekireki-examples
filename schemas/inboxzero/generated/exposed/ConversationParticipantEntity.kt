package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class ConversationParticipantEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ConversationParticipantEntity>(ConversationParticipantTable)

    var conversationId by ConversationParticipantTable.conversationId
    var conversation by ConversationEntity referencedOn ConversationParticipantTable.conversationId
    var memberId by ConversationParticipantTable.memberId
    var member by MemberEntity referencedOn ConversationParticipantTable.memberId
    var generation by ConversationParticipantTable.generation
    var active by ConversationParticipantTable.active
    var readRevision by ConversationParticipantTable.readRevision
    var muted by ConversationParticipantTable.muted
    var createdAt by ConversationParticipantTable.createdAt
    val activity by ConversationActivityEntity referrersOn ConversationActivityTable.participantId
}
