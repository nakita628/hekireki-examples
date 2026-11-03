package papermark.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class ChatMessageEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ChatMessageEntity>(ChatMessageTable)

    var chatId by ChatMessageTable.chatId
    var chat by ChatEntity referencedOn ChatMessageTable.chatId
    var role by ChatMessageTable.role
    var content by ChatMessageTable.content
    var metadata by ChatMessageTable.metadata
    var createdAt by ChatMessageTable.createdAt
}
