package openpanel.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class ChatMessageEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ChatMessageEntity>(ChatMessageTable)

    var conversationId by ChatMessageTable.conversationId
    var conversation by ConversationEntity referencedOn ChatMessageTable.conversationId
    var role by ChatMessageTable.role
    var parts by ChatMessageTable.parts
    var metadata by ChatMessageTable.metadata
    var createdAt by ChatMessageTable.createdAt
}
