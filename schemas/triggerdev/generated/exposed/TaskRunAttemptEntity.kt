package triggerdev.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class TaskRunAttemptEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, TaskRunAttemptEntity>(TaskRunAttemptTable)

    var number by TaskRunAttemptTable.number
    var friendlyId by TaskRunAttemptTable.friendlyId
    var taskRun by TaskRunEntity referencedOn TaskRunAttemptTable.taskRunId
    var taskRunId by TaskRunAttemptTable.taskRunId
    var backgroundWorker by BackgroundWorkerEntity referencedOn TaskRunAttemptTable.backgroundWorkerId
    var backgroundWorkerId by TaskRunAttemptTable.backgroundWorkerId
    var backgroundWorkerTask by BackgroundWorkerTaskEntity referencedOn TaskRunAttemptTable.backgroundWorkerTaskId
    var backgroundWorkerTaskId by TaskRunAttemptTable.backgroundWorkerTaskId
    var runtimeEnvironment by RuntimeEnvironmentEntity referencedOn TaskRunAttemptTable.runtimeEnvironmentId
    var runtimeEnvironmentId by TaskRunAttemptTable.runtimeEnvironmentId
    var queue by TaskQueueEntity referencedOn TaskRunAttemptTable.queueId
    var queueId by TaskRunAttemptTable.queueId
    var status by TaskRunAttemptTable.status
    var createdAt by TaskRunAttemptTable.createdAt
    var updatedAt by TaskRunAttemptTable.updatedAt
    var startedAt by TaskRunAttemptTable.startedAt
    var completedAt by TaskRunAttemptTable.completedAt
    var usageDurationMs by TaskRunAttemptTable.usageDurationMs
    var error by TaskRunAttemptTable.error
    var output by TaskRunAttemptTable.output
    var outputType by TaskRunAttemptTable.outputType
    val dependencies by TaskRunDependencyEntity optionalReferrersOn TaskRunDependencyTable.dependentAttemptId
    val batchDependencies by BatchTaskRunEntity optionalReferrersOn BatchTaskRunTable.dependentTaskAttemptId
    val checkpoints by CheckpointEntity referrersOn CheckpointTable.attemptId
    val batchTaskRunItems by BatchTaskRunItemEntity optionalReferrersOn BatchTaskRunItemTable.taskRunAttemptId
    val checkpointRestoreEvent by CheckpointRestoreEventEntity referrersOn CheckpointRestoreEventTable.attemptId
    val childRuns by TaskRunEntity optionalReferrersOn TaskRunTable.parentTaskRunAttemptId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(TaskRunAttemptTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
