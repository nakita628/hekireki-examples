package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class ChatActionTotalEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ChatActionTotalEntity>(ChatActionTotalTable)

    var createdAt by ChatActionTotalTable.createdAt
    var updatedAt by ChatActionTotalTable.updatedAt
    var action by ChatActionTotalTable.action
    var count by ChatActionTotalTable.count
    var chatId by ChatActionTotalTable.chatId
    var chat by ChatEntity referencedOn ChatActionTotalTable.chatId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ChatActionTotalTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
