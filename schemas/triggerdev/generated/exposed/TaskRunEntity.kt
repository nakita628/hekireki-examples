package triggerdev.models

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

    var number by TaskRunTable.number
    var friendlyId by TaskRunTable.friendlyId
    var engine by TaskRunTable.engine
    var status by TaskRunTable.status
    var statusReason by TaskRunTable.statusReason
    var idempotencyKey by TaskRunTable.idempotencyKey
    var idempotencyKeyExpiresAt by TaskRunTable.idempotencyKeyExpiresAt
    var idempotencyKeyOptions by TaskRunTable.idempotencyKeyOptions
    var debounce by TaskRunTable.debounce
    var taskIdentifier by TaskRunTable.taskIdentifier
    var isTest by TaskRunTable.isTest
    var payload by TaskRunTable.payload
    var payloadType by TaskRunTable.payloadType
    var context by TaskRunTable.context
    var traceContext by TaskRunTable.traceContext
    var traceId by TaskRunTable.traceId
    var spanId by TaskRunTable.spanId
    var runtimeEnvironment by RuntimeEnvironmentEntity referencedOn TaskRunTable.runtimeEnvironmentId
    var runtimeEnvironmentId by TaskRunTable.runtimeEnvironmentId
    var environmentType by TaskRunTable.environmentType
    var project by ProjectEntity referencedOn TaskRunTable.projectId
    var projectId by TaskRunTable.projectId
    var organizationId by TaskRunTable.organizationId
    var queue by TaskRunTable.queue
    var lockedQueueId by TaskRunTable.lockedQueueId
    var workerQueue by TaskRunTable.workerQueue
    var region by TaskRunTable.region
    var secondaryMasterQueue by TaskRunTable.secondaryMasterQueue
    var attemptNumber by TaskRunTable.attemptNumber
    var createdAt by TaskRunTable.createdAt
    var updatedAt by TaskRunTable.updatedAt
    val attempts by TaskRunAttemptEntity referrersOn TaskRunAttemptTable.taskRunId
    var runTags by TaskRunTable.runTags
    var taskVersion by TaskRunTable.taskVersion
    var sdkVersion by TaskRunTable.sdkVersion
    var cliVersion by TaskRunTable.cliVersion
    val checkpoints by CheckpointEntity referrersOn CheckpointTable.runId
    var startedAt by TaskRunTable.startedAt
    var executedAt by TaskRunTable.executedAt
    var completedAt by TaskRunTable.completedAt
    var machinePreset by TaskRunTable.machinePreset
    var usageDurationMs by TaskRunTable.usageDurationMs
    var costInCents by TaskRunTable.costInCents
    var baseCostInCents by TaskRunTable.baseCostInCents
    var lockedAt by TaskRunTable.lockedAt
    var lockedBy by BackgroundWorkerTaskEntity optionalReferencedOn TaskRunTable.lockedById
    var lockedById by TaskRunTable.lockedById
    var lockedToVersion by BackgroundWorkerEntity optionalReferencedOn TaskRunTable.lockedToVersionId
    var lockedToVersionId by TaskRunTable.lockedToVersionId
    var priorityMs by TaskRunTable.priorityMs
    var concurrencyKey by TaskRunTable.concurrencyKey
    var gates by TaskRunTable.gates
    var delayUntil by TaskRunTable.delayUntil
    var queuedAt by TaskRunTable.queuedAt
    var ttl by TaskRunTable.ttl
    var expiredAt by TaskRunTable.expiredAt
    var maxAttempts by TaskRunTable.maxAttempts
    var lockedRetryConfig by TaskRunTable.lockedRetryConfig
    var oneTimeUseToken by TaskRunTable.oneTimeUseToken
    val associatedWaitpoint by WaitpointEntity optionalBackReferencedOn WaitpointTable.completedByTaskRunId
    val blockedByWaitpoints by TaskRunWaitpointEntity referrersOn TaskRunWaitpointTable.taskRunId
    var connectedWaitpoints by WaitpointEntity via WaitpointRunConnectionsTable
    var taskEventStore by TaskRunTable.taskEventStore
    var queueTimestamp by TaskRunTable.queueTimestamp
    val batchItems by BatchTaskRunItemEntity referrersOn BatchTaskRunItemTable.taskRunId
    val dependency by TaskRunDependencyEntity optionalBackReferencedOn TaskRunDependencyTable.taskRunId
    val checkpointRestoreEvent by CheckpointRestoreEventEntity referrersOn CheckpointRestoreEventTable.runId
    val executionSnapshots by TaskRunExecutionSnapshotEntity referrersOn TaskRunExecutionSnapshotTable.runId
    var scheduleInstanceId by TaskRunTable.scheduleInstanceId
    var scheduleId by TaskRunTable.scheduleId
    val sourceBulkActionItems by BulkActionItemEntity referrersOn BulkActionItemTable.sourceRunId
    val destinationBulkActionItems by BulkActionItemEntity optionalReferrersOn BulkActionItemTable.destinationRunId
    var bulkActionGroupIds by TaskRunTable.bulkActionGroupIds
    var logsDeletedAt by TaskRunTable.logsDeletedAt
    var replayedFromTaskRunFriendlyId by TaskRunTable.replayedFromTaskRunFriendlyId
    var rootTaskRun by TaskRunEntity optionalReferencedOn TaskRunTable.rootTaskRunId
    var rootTaskRunId by TaskRunTable.rootTaskRunId
    val descendantRuns by TaskRunEntity optionalReferrersOn TaskRunTable.rootTaskRunId
    var parentTaskRun by TaskRunEntity optionalReferencedOn TaskRunTable.parentTaskRunId
    var parentTaskRunId by TaskRunTable.parentTaskRunId
    val childRuns by TaskRunEntity optionalReferrersOn TaskRunTable.parentTaskRunId
    var parentTaskRunAttempt by TaskRunAttemptEntity optionalReferencedOn TaskRunTable.parentTaskRunAttemptId
    var parentTaskRunAttemptId by TaskRunTable.parentTaskRunAttemptId
    var batch by BatchTaskRunEntity optionalReferencedOn TaskRunTable.batchId
    var batchId by TaskRunTable.batchId
    var resumeParentOnCompletion by TaskRunTable.resumeParentOnCompletion
    var depth by TaskRunTable.depth
    var parentSpanId by TaskRunTable.parentSpanId
    var runChainState by TaskRunTable.runChainState
    var seedMetadata by TaskRunTable.seedMetadata
    var seedMetadataType by TaskRunTable.seedMetadataType
    var metadata by TaskRunTable.metadata
    var metadataType by TaskRunTable.metadataType
    var metadataVersion by TaskRunTable.metadataVersion
    var annotations by TaskRunTable.annotations
    var isWarmStart by TaskRunTable.isWarmStart
    var output by TaskRunTable.output
    var outputType by TaskRunTable.outputType
    var error by TaskRunTable.error
    var planType by TaskRunTable.planType
    var maxDurationInSeconds by TaskRunTable.maxDurationInSeconds
    var realtimeStreamsVersion by TaskRunTable.realtimeStreamsVersion
    var realtimeStreams by TaskRunTable.realtimeStreams
    var streamBasinName by TaskRunTable.streamBasinName
    val playgroundConversations by PlaygroundConversationEntity optionalReferrersOn PlaygroundConversationTable.runId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(TaskRunTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
