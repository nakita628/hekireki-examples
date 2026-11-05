package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class CleanupThreadEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, CleanupThreadEntity>(CleanupThreadTable)

    var createdAt by CleanupThreadTable.createdAt
    var updatedAt by CleanupThreadTable.updatedAt
    var threadId by CleanupThreadTable.threadId
    var archived by CleanupThreadTable.archived
    var jobId by CleanupThreadTable.jobId
    var job by CleanupJobEntity referencedOn CleanupThreadTable.jobId
    var emailAccountId by CleanupThreadTable.emailAccountId
    var emailAccount by EmailAccountEntity referencedOn CleanupThreadTable.emailAccountId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(CleanupThreadTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
