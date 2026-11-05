package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class ChatMessageEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ChatMessageEntity>(ChatMessageTable)

    var createdAt by ChatMessageTable.createdAt
    var updatedAt by ChatMessageTable.updatedAt
    var role by ChatMessageTable.role
    var parts by ChatMessageTable.parts
    var metadata by ChatMessageTable.metadata
    var chatId by ChatMessageTable.chatId
    var chat by ChatEntity referencedOn ChatMessageTable.chatId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ChatMessageTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
