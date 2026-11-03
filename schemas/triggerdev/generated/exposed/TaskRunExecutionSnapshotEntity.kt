package triggerdev.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class TaskRunExecutionSnapshotEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, TaskRunExecutionSnapshotEntity>(TaskRunExecutionSnapshotTable)

    var engine by TaskRunExecutionSnapshotTable.engine
    var executionStatus by TaskRunExecutionSnapshotTable.executionStatus
    var description by TaskRunExecutionSnapshotTable.description
    var isValid by TaskRunExecutionSnapshotTable.isValid
    var error by TaskRunExecutionSnapshotTable.error
    var previousSnapshotId by TaskRunExecutionSnapshotTable.previousSnapshotId
    var runId by TaskRunExecutionSnapshotTable.runId
    var run by TaskRunEntity referencedOn TaskRunExecutionSnapshotTable.runId
    var runStatus by TaskRunExecutionSnapshotTable.runStatus
    var batchId by TaskRunExecutionSnapshotTable.batchId
    var attemptNumber by TaskRunExecutionSnapshotTable.attemptNumber
    var environmentId by TaskRunExecutionSnapshotTable.environmentId
    var environmentType by TaskRunExecutionSnapshotTable.environmentType
    var projectId by TaskRunExecutionSnapshotTable.projectId
    var organizationId by TaskRunExecutionSnapshotTable.organizationId
    var completedWaitpoints by WaitpointEntity via CompletedWaitpointsTable
    var completedWaitpointOrder by TaskRunExecutionSnapshotTable.completedWaitpointOrder
    var checkpointId by TaskRunExecutionSnapshotTable.checkpointId
    var checkpoint by TaskRunCheckpointEntity optionalReferencedOn TaskRunExecutionSnapshotTable.checkpointId
    var workerId by TaskRunExecutionSnapshotTable.workerId
    var runnerId by TaskRunExecutionSnapshotTable.runnerId
    var createdAt by TaskRunExecutionSnapshotTable.createdAt
    var updatedAt by TaskRunExecutionSnapshotTable.updatedAt
    var lastHeartbeatAt by TaskRunExecutionSnapshotTable.lastHeartbeatAt
    var metadata by TaskRunExecutionSnapshotTable.metadata

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(TaskRunExecutionSnapshotTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
