package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class ReplyMemoryEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ReplyMemoryEntity>(ReplyMemoryTable)

    var createdAt by ReplyMemoryTable.createdAt
    var updatedAt by ReplyMemoryTable.updatedAt
    var content by ReplyMemoryTable.content
    var kind by ReplyMemoryTable.kind
    var scopeType by ReplyMemoryTable.scopeType
    var scopeValue by ReplyMemoryTable.scopeValue
    var isLearnedStyleEvidence by ReplyMemoryTable.isLearnedStyleEvidence
    var emailAccountId by ReplyMemoryTable.emailAccountId
    var emailAccount by EmailAccountEntity referencedOn ReplyMemoryTable.emailAccountId
    val sources by ReplyMemorySourceEntity referrersOn ReplyMemorySourceTable.replyMemoryId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ReplyMemoryTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
