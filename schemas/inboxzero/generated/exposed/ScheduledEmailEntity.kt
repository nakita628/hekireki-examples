package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class ScheduledEmailEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ScheduledEmailEntity>(ScheduledEmailTable)

    var createdAt by ScheduledEmailTable.createdAt
    var updatedAt by ScheduledEmailTable.updatedAt
    var emailAccountId by ScheduledEmailTable.emailAccountId
    var emailAccount by EmailAccountEntity referencedOn ScheduledEmailTable.emailAccountId
    var clientMutationId by ScheduledEmailTable.clientMutationId
    var payloadHash by ScheduledEmailTable.payloadHash
    var payload by ScheduledEmailTable.payload
    var threadId by ScheduledEmailTable.threadId
    var sendAt by ScheduledEmailTable.sendAt
    var status by ScheduledEmailTable.status
    var processingStartedAt by ScheduledEmailTable.processingStartedAt
    var executionQueuedAt by ScheduledEmailTable.executionQueuedAt
    var sentAt by ScheduledEmailTable.sentAt
    var error by ScheduledEmailTable.error
    var remindAt by ScheduledEmailTable.remindAt
    var reminderStatus by ScheduledEmailTable.reminderStatus
    var reminderStartedAt by ScheduledEmailTable.reminderStartedAt
    var heldForUndo by ScheduledEmailTable.heldForUndo
    var draftMessageIds by ScheduledEmailTable.draftMessageIds

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ScheduledEmailTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
