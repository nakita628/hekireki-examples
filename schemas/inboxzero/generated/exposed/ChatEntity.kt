package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class ChatEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ChatEntity>(ChatTable)

    var createdAt by ChatTable.createdAt
    var updatedAt by ChatTable.updatedAt
    var deletedAt by ChatTable.deletedAt
    var name by ChatTable.name
    var compactionCount by ChatTable.compactionCount
    var lastSeenRulesRevision by ChatTable.lastSeenRulesRevision
    var activeStreamId by ChatTable.activeStreamId
    var activeStreamStartedAt by ChatTable.activeStreamStartedAt
    val messages by ChatMessageEntity referrersOn ChatMessageTable.chatId
    val compactions by ChatCompactionEntity referrersOn ChatCompactionTable.chatId
    val memories by ChatMemoryEntity optionalReferrersOn ChatMemoryTable.chatId
    val actionTotals by ChatActionTotalEntity referrersOn ChatActionTotalTable.chatId
    var emailAccountId by ChatTable.emailAccountId
    var emailAccount by EmailAccountEntity referencedOn ChatTable.emailAccountId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ChatTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
