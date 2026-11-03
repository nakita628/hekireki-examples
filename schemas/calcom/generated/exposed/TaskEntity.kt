package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class TaskEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, TaskEntity>(TaskTable)

    var createdAt by TaskTable.createdAt
    var updatedAt by TaskTable.updatedAt
    var scheduledAt by TaskTable.scheduledAt
    var succeededAt by TaskTable.succeededAt
    var type by TaskTable.type
    var payload by TaskTable.payload
    var attempts by TaskTable.attempts
    var maxAttempts by TaskTable.maxAttempts
    var lastError by TaskTable.lastError
    var lastFailedAttemptAt by TaskTable.lastFailedAttemptAt
    var referenceUid by TaskTable.referenceUid

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(TaskTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
