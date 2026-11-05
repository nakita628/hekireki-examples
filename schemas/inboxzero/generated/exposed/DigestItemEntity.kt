package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class DigestItemEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, DigestItemEntity>(DigestItemTable)

    var createdAt by DigestItemTable.createdAt
    var updatedAt by DigestItemTable.updatedAt
    var messageId by DigestItemTable.messageId
    var threadId by DigestItemTable.threadId
    var content by DigestItemTable.content
    var digestId by DigestItemTable.digestId
    var digest by DigestEntity referencedOn DigestItemTable.digestId
    var actionId by DigestItemTable.actionId
    var action by ExecutedActionEntity optionalReferencedOn DigestItemTable.actionId
    var coldEmailId by DigestItemTable.coldEmailId
    var coldEmail by ColdEmailEntity optionalReferencedOn DigestItemTable.coldEmailId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(DigestItemTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
