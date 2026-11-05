package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class EmailMessageEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, EmailMessageEntity>(EmailMessageTable)

    var createdAt by EmailMessageTable.createdAt
    var updatedAt by EmailMessageTable.updatedAt
    var threadId by EmailMessageTable.threadId
    var messageId by EmailMessageTable.messageId
    var date by EmailMessageTable.date
    var from by EmailMessageTable.from
    var fromName by EmailMessageTable.fromName
    var fromDomain by EmailMessageTable.fromDomain
    var to by EmailMessageTable.to
    var unsubscribeLink by EmailMessageTable.unsubscribeLink
    var read by EmailMessageTable.read
    var sent by EmailMessageTable.sent
    var draft by EmailMessageTable.draft
    var inbox by EmailMessageTable.inbox
    var emailAccountId by EmailMessageTable.emailAccountId
    var emailAccount by EmailAccountEntity referencedOn EmailMessageTable.emailAccountId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(EmailMessageTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
