package blinko.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class ConversationEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, ConversationEntity>(ConversationTable)

    var title by ConversationTable.title
    var isShare by ConversationTable.isShare
    var accountId by ConversationTable.accountId
    var createdAt by ConversationTable.createdAt
    var updatedAt by ConversationTable.updatedAt
    val messages by MessageEntity referrersOn MessageTable.conversationId
    var account by AccountsEntity referencedOn ConversationTable.accountId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ConversationTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
