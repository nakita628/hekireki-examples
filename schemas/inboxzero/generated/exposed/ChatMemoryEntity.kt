package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class ChatMemoryEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ChatMemoryEntity>(ChatMemoryTable)

    var createdAt by ChatMemoryTable.createdAt
    var updatedAt by ChatMemoryTable.updatedAt
    var content by ChatMemoryTable.content
    var chatId by ChatMemoryTable.chatId
    var chat by ChatEntity optionalReferencedOn ChatMemoryTable.chatId
    var emailAccountId by ChatMemoryTable.emailAccountId
    var emailAccount by EmailAccountEntity referencedOn ChatMemoryTable.emailAccountId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ChatMemoryTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
