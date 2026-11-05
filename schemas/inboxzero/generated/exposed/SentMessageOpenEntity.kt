package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class SentMessageOpenEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, SentMessageOpenEntity>(SentMessageOpenTable)

    var createdAt by SentMessageOpenTable.createdAt
    var updatedAt by SentMessageOpenTable.updatedAt
    var token by SentMessageOpenTable.token
    var emailAccountId by SentMessageOpenTable.emailAccountId
    var emailAccount by EmailAccountEntity referencedOn SentMessageOpenTable.emailAccountId
    var threadId by SentMessageOpenTable.threadId
    var messageId by SentMessageOpenTable.messageId
    var firstOpenedAt by SentMessageOpenTable.firstOpenedAt
    var lastOpenedAt by SentMessageOpenTable.lastOpenedAt
    var openCount by SentMessageOpenTable.openCount

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(SentMessageOpenTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
