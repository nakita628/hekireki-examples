package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class BulkInboxActionJobEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, BulkInboxActionJobEntity>(BulkInboxActionJobTable)

    var createdAt by BulkInboxActionJobTable.createdAt
    var updatedAt by BulkInboxActionJobTable.updatedAt
    var status by BulkInboxActionJobTable.status
    var search by BulkInboxActionJobTable.search
    var action by BulkInboxActionJobTable.action
    var labelId by BulkInboxActionJobTable.labelId
    var labelName by BulkInboxActionJobTable.labelName
    var instruction by BulkInboxActionJobTable.instruction
    var pageToken by BulkInboxActionJobTable.pageToken
    var rateLimitedUntil by BulkInboxActionJobTable.rateLimitedUntil
    var leaseExpiresAt by BulkInboxActionJobTable.leaseExpiresAt
    var chunkToken by BulkInboxActionJobTable.chunkToken
    var recoveryCount by BulkInboxActionJobTable.recoveryCount
    var processedCount by BulkInboxActionJobTable.processedCount
    var matchedCount by BulkInboxActionJobTable.matchedCount
    var actedCount by BulkInboxActionJobTable.actedCount
    var skippedCount by BulkInboxActionJobTable.skippedCount
    var failedCount by BulkInboxActionJobTable.failedCount
    var limitReached by BulkInboxActionJobTable.limitReached
    var error by BulkInboxActionJobTable.error
    var completedAt by BulkInboxActionJobTable.completedAt
    var confirmationKey by BulkInboxActionJobTable.confirmationKey
    var chatId by BulkInboxActionJobTable.chatId
    var chat by ChatEntity optionalReferencedOn BulkInboxActionJobTable.chatId
    val handledThreads by BulkInboxActionThreadEntity referrersOn BulkInboxActionThreadTable.jobId
    var emailAccountId by BulkInboxActionJobTable.emailAccountId
    var emailAccount by EmailAccountEntity referencedOn BulkInboxActionJobTable.emailAccountId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(BulkInboxActionJobTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
