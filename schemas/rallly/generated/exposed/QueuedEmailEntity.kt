package rallly.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class QueuedEmailEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, QueuedEmailEntity>(QueuedEmailTable)

    var kind by QueuedEmailTable.kind
    var subjectId by QueuedEmailTable.subjectId
    var batchId by QueuedEmailTable.batchId
    var userId by QueuedEmailTable.userId
    var status by QueuedEmailTable.status
    var sendAfter by QueuedEmailTable.sendAfter
    var attempts by QueuedEmailTable.attempts
    var claimedAt by QueuedEmailTable.claimedAt
    var sentAt by QueuedEmailTable.sentAt
    var lastError by QueuedEmailTable.lastError
    var createdAt by QueuedEmailTable.createdAt
    var updatedAt by QueuedEmailTable.updatedAt
    var user by UserEntity referencedOn QueuedEmailTable.userId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(QueuedEmailTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
