package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class ScheduledJobEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ScheduledJobEntity>(ScheduledJobTable)

    var projectId by ScheduledJobTable.projectId
    var targetType by ScheduledJobTable.targetType
    var targetId by ScheduledJobTable.targetId
    var cron by ScheduledJobTable.cron
    var timezone by ScheduledJobTable.timezone
    var nextRunAt by ScheduledJobTable.nextRunAt
    var lastSlot by ScheduledJobTable.lastSlot
    var currentSlot by ScheduledJobTable.currentSlot
    var attempts by ScheduledJobTable.attempts
    var lastError by ScheduledJobTable.lastError
    var active by ScheduledJobTable.active
    var createdAt by ScheduledJobTable.createdAt
    var updatedAt by ScheduledJobTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ScheduledJobTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
