package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class ChatCompactionEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ChatCompactionEntity>(ChatCompactionTable)

    var createdAt by ChatCompactionTable.createdAt
    var summary by ChatCompactionTable.summary
    var messageCount by ChatCompactionTable.messageCount
    var compactedBeforeCreatedAt by ChatCompactionTable.compactedBeforeCreatedAt
    var chatId by ChatCompactionTable.chatId
    var chat by ChatEntity referencedOn ChatCompactionTable.chatId
}
