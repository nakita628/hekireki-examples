package triggerdev.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class TaskRunDependencyEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, TaskRunDependencyEntity>(TaskRunDependencyTable)

    var taskRun by TaskRunEntity referencedOn TaskRunDependencyTable.taskRunId
    var taskRunId by TaskRunDependencyTable.taskRunId
    var checkpointEvent by CheckpointRestoreEventEntity optionalReferencedOn TaskRunDependencyTable.checkpointEventId
    var checkpointEventId by TaskRunDependencyTable.checkpointEventId
    var dependentAttempt by TaskRunAttemptEntity optionalReferencedOn TaskRunDependencyTable.dependentAttemptId
    var dependentAttemptId by TaskRunDependencyTable.dependentAttemptId
    var dependentBatchRun by BatchTaskRunEntity optionalReferencedOn TaskRunDependencyTable.dependentBatchRunId
    var dependentBatchRunId by TaskRunDependencyTable.dependentBatchRunId
    var createdAt by TaskRunDependencyTable.createdAt
    var updatedAt by TaskRunDependencyTable.updatedAt
    var resumedAt by TaskRunDependencyTable.resumedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(TaskRunDependencyTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
