package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class CleanupJobEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, CleanupJobEntity>(CleanupJobTable)

    var createdAt by CleanupJobTable.createdAt
    var updatedAt by CleanupJobTable.updatedAt
    var action by CleanupJobTable.action
    var daysOld by CleanupJobTable.daysOld
    var instructions by CleanupJobTable.instructions
    var skipReply by CleanupJobTable.skipReply
    var skipStarred by CleanupJobTable.skipStarred
    var skipCalendar by CleanupJobTable.skipCalendar
    var skipReceipt by CleanupJobTable.skipReceipt
    var skipAttachment by CleanupJobTable.skipAttachment
    var skipConversation by CleanupJobTable.skipConversation
    var emailAccountId by CleanupJobTable.emailAccountId
    var emailAccount by EmailAccountEntity referencedOn CleanupJobTable.emailAccountId
    val threads by CleanupThreadEntity referrersOn CleanupThreadTable.jobId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(CleanupJobTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
