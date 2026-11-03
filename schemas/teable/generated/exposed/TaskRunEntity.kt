package teable.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class TaskRunEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, TaskRunEntity>(TaskRunTable)

    var taskId by TaskRunTable.taskId
    var baseId by TaskRunTable.baseId
    var status by TaskRunTable.status
    var snapshot by TaskRunTable.snapshot
    var dependsOnRunIds by TaskRunTable.dependsOnRunIds
    var spent by TaskRunTable.spent
    var log by TaskRunTable.log
    var errorMsg by TaskRunTable.errorMsg
    var startedTime by TaskRunTable.startedTime
    var createdTime by TaskRunTable.createdTime
    var lastModifiedTime by TaskRunTable.lastModifiedTime
    var task by TaskEntity referencedOn TaskRunTable.taskId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(TaskRunTable.lastModifiedTime)) {
                lastModifiedTime = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
