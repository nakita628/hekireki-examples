package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class ThreadTrackerEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ThreadTrackerEntity>(ThreadTrackerTable)

    var createdAt by ThreadTrackerTable.createdAt
    var updatedAt by ThreadTrackerTable.updatedAt
    var sentAt by ThreadTrackerTable.sentAt
    var threadId by ThreadTrackerTable.threadId
    var messageId by ThreadTrackerTable.messageId
    var resolved by ThreadTrackerTable.resolved
    var type by ThreadTrackerTable.type
    var followUpAppliedAt by ThreadTrackerTable.followUpAppliedAt
    var followUpDraftId by ThreadTrackerTable.followUpDraftId
    var followUpDraftContent by ThreadTrackerTable.followUpDraftContent
    var followUpDraftCreatedAt by ThreadTrackerTable.followUpDraftCreatedAt
    var followUpNotifications by ThreadTrackerTable.followUpNotifications
    var emailAccountId by ThreadTrackerTable.emailAccountId
    var emailAccount by EmailAccountEntity referencedOn ThreadTrackerTable.emailAccountId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ThreadTrackerTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
