package triggerdev.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class BatchTaskRunItemEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, BatchTaskRunItemEntity>(BatchTaskRunItemTable)

    var status by BatchTaskRunItemTable.status
    var batchTaskRun by BatchTaskRunEntity referencedOn BatchTaskRunItemTable.batchTaskRunId
    var batchTaskRunId by BatchTaskRunItemTable.batchTaskRunId
    var taskRun by TaskRunEntity referencedOn BatchTaskRunItemTable.taskRunId
    var taskRunId by BatchTaskRunItemTable.taskRunId
    var taskRunAttempt by TaskRunAttemptEntity optionalReferencedOn BatchTaskRunItemTable.taskRunAttemptId
    var taskRunAttemptId by BatchTaskRunItemTable.taskRunAttemptId
    var createdAt by BatchTaskRunItemTable.createdAt
    var updatedAt by BatchTaskRunItemTable.updatedAt
    var completedAt by BatchTaskRunItemTable.completedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(BatchTaskRunItemTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
