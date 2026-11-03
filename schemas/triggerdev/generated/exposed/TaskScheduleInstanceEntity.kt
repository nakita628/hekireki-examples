package triggerdev.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class TaskScheduleInstanceEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, TaskScheduleInstanceEntity>(TaskScheduleInstanceTable)

    var taskSchedule by TaskScheduleEntity referencedOn TaskScheduleInstanceTable.taskScheduleId
    var taskScheduleId by TaskScheduleInstanceTable.taskScheduleId
    var environment by RuntimeEnvironmentEntity referencedOn TaskScheduleInstanceTable.environmentId
    var environmentId by TaskScheduleInstanceTable.environmentId
    var project by ProjectEntity referencedOn TaskScheduleInstanceTable.projectId
    var projectId by TaskScheduleInstanceTable.projectId
    var schedulePhase by TaskScheduleInstanceTable.schedulePhase
    var createdAt by TaskScheduleInstanceTable.createdAt
    var updatedAt by TaskScheduleInstanceTable.updatedAt
    var active by TaskScheduleInstanceTable.active
    var lastScheduledTimestamp by TaskScheduleInstanceTable.lastScheduledTimestamp
    var nextScheduledTimestamp by TaskScheduleInstanceTable.nextScheduledTimestamp

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(TaskScheduleInstanceTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
