package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class ResponseTimeEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ResponseTimeEntity>(ResponseTimeTable)

    var createdAt by ResponseTimeTable.createdAt
    var threadId by ResponseTimeTable.threadId
    var sentMessageId by ResponseTimeTable.sentMessageId
    var receivedMessageId by ResponseTimeTable.receivedMessageId
    var responseTimeMins by ResponseTimeTable.responseTimeMins
    var receivedAt by ResponseTimeTable.receivedAt
    var sentAt by ResponseTimeTable.sentAt
    var emailAccountId by ResponseTimeTable.emailAccountId
    var emailAccount by EmailAccountEntity referencedOn ResponseTimeTable.emailAccountId
}
