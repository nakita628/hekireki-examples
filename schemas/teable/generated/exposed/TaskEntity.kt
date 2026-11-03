package teable.models

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

    var type by TaskTable.type
    var status by TaskTable.status
    var snapshot by TaskTable.snapshot
    var createdTime by TaskTable.createdTime
    var lastModifiedTime by TaskTable.lastModifiedTime
    var createdBy by TaskTable.createdBy
    var lastModifiedBy by TaskTable.lastModifiedBy
    val runs by TaskRunEntity referrersOn TaskRunTable.taskId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(TaskTable.lastModifiedTime)) {
                lastModifiedTime = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
