package triggerdev.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class BatchTaskRunEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, BatchTaskRunEntity>(BatchTaskRunTable)

    var friendlyId by BatchTaskRunTable.friendlyId
    var idempotencyKey by BatchTaskRunTable.idempotencyKey
    var idempotencyKeyExpiresAt by BatchTaskRunTable.idempotencyKeyExpiresAt
    var status by BatchTaskRunTable.status
    var runtimeEnvironmentId by BatchTaskRunTable.runtimeEnvironmentId
    val runs by TaskRunEntity optionalReferrersOn TaskRunTable.batchId
    var createdAt by BatchTaskRunTable.createdAt
    var updatedAt by BatchTaskRunTable.updatedAt
    var runIds by BatchTaskRunTable.runIds
    var runCount by BatchTaskRunTable.runCount
    var payload by BatchTaskRunTable.payload
    var payloadType by BatchTaskRunTable.payloadType
    var options by BatchTaskRunTable.options1
    var batchVersion by BatchTaskRunTable.batchVersion
    val runsBlocked by TaskRunWaitpointEntity optionalReferrersOn TaskRunWaitpointTable.batchId
    val waitpoints by WaitpointEntity optionalReferrersOn WaitpointTable.completedByBatchId
    var sealed by BatchTaskRunTable.sealed
    var sealedAt by BatchTaskRunTable.sealedAt
    var expectedCount by BatchTaskRunTable.expectedCount
    var completedCount by BatchTaskRunTable.completedCount
    var completedAt by BatchTaskRunTable.completedAt
    var resumedAt by BatchTaskRunTable.resumedAt
    var processingJobsCount by BatchTaskRunTable.processingJobsCount
    var processingJobsExpectedCount by BatchTaskRunTable.processingJobsExpectedCount
    var oneTimeUseToken by BatchTaskRunTable.oneTimeUseToken
    var processingStartedAt by BatchTaskRunTable.processingStartedAt
    var processingCompletedAt by BatchTaskRunTable.processingCompletedAt
    var successfulRunCount by BatchTaskRunTable.successfulRunCount
    var failedRunCount by BatchTaskRunTable.failedRunCount
    val errors by BatchTaskRunErrorEntity referrersOn BatchTaskRunErrorTable.batchTaskRunId
    val items by BatchTaskRunItemEntity referrersOn BatchTaskRunItemTable.batchTaskRunId
    var taskIdentifier by BatchTaskRunTable.taskIdentifier
    var checkpointEvent by CheckpointRestoreEventEntity optionalReferencedOn BatchTaskRunTable.checkpointEventId
    var checkpointEventId by BatchTaskRunTable.checkpointEventId
    var dependentTaskAttempt by TaskRunAttemptEntity optionalReferencedOn BatchTaskRunTable.dependentTaskAttemptId
    var dependentTaskAttemptId by BatchTaskRunTable.dependentTaskAttemptId
    val runDependencies by TaskRunDependencyEntity optionalReferrersOn TaskRunDependencyTable.dependentBatchRunId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(BatchTaskRunTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
