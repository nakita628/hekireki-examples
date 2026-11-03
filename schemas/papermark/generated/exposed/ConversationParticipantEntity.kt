package papermark.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class ConversationParticipantEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ConversationParticipantEntity>(ConversationParticipantTable)

    var conversationId by ConversationParticipantTable.conversationId
    var conversation by ConversationEntity referencedOn ConversationParticipantTable.conversationId
    var role by ConversationParticipantTable.role
    var viewerId by ConversationParticipantTable.viewerId
    var viewer by ViewerEntity optionalReferencedOn ConversationParticipantTable.viewerId
    var userId by ConversationParticipantTable.userId
    var user by UserEntity optionalReferencedOn ConversationParticipantTable.userId
    var receiveNotifications by ConversationParticipantTable.receiveNotifications
    var createdAt by ConversationParticipantTable.createdAt
}
