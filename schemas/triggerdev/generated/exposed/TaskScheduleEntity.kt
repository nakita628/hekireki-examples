package triggerdev.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class TaskScheduleEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, TaskScheduleEntity>(TaskScheduleTable)

    var type by TaskScheduleTable.type
    var friendlyId by TaskScheduleTable.friendlyId
    var taskIdentifier by TaskScheduleTable.taskIdentifier
    var deduplicationKey by TaskScheduleTable.deduplicationKey
    var userProvidedDeduplicationKey by TaskScheduleTable.userProvidedDeduplicationKey
    var generatorExpression by TaskScheduleTable.generatorExpression
    var generatorDescription by TaskScheduleTable.generatorDescription
    var generatorType by TaskScheduleTable.generatorType
    var timezone by TaskScheduleTable.timezone
    var windowDurationSeconds by TaskScheduleTable.windowDurationSeconds
    var windowPercentage by TaskScheduleTable.windowPercentage
    var defaultWindowDurationSeconds by TaskScheduleTable.defaultWindowDurationSeconds
    var minimumWindowDurationSeconds by TaskScheduleTable.minimumWindowDurationSeconds
    var externalId by TaskScheduleTable.externalId
    val instances by TaskScheduleInstanceEntity referrersOn TaskScheduleInstanceTable.taskScheduleId
    var lastRunTriggeredAt by TaskScheduleTable.lastRunTriggeredAt
    var project by ProjectEntity referencedOn TaskScheduleTable.projectId
    var projectId by TaskScheduleTable.projectId
    var createdAt by TaskScheduleTable.createdAt
    var updatedAt by TaskScheduleTable.updatedAt
    var active by TaskScheduleTable.active

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(TaskScheduleTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
