package triggerdev.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class WaitpointEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, WaitpointEntity>(WaitpointTable)

    var friendlyId by WaitpointTable.friendlyId
    var type by WaitpointTable.type
    var status by WaitpointTable.status
    var completedAt by WaitpointTable.completedAt
    var idempotencyKey by WaitpointTable.idempotencyKey
    var userProvidedIdempotencyKey by WaitpointTable.userProvidedIdempotencyKey
    var idempotencyKeyExpiresAt by WaitpointTable.idempotencyKeyExpiresAt
    var inactiveIdempotencyKey by WaitpointTable.inactiveIdempotencyKey
    var completedByTaskRunId by WaitpointTable.completedByTaskRunId
    var completedByTaskRun by TaskRunEntity optionalReferencedOn WaitpointTable.completedByTaskRunId
    var completedAfter by WaitpointTable.completedAfter
    var completedByBatchId by WaitpointTable.completedByBatchId
    var completedByBatch by BatchTaskRunEntity optionalReferencedOn WaitpointTable.completedByBatchId
    val blockingTaskRuns by TaskRunWaitpointEntity referrersOn TaskRunWaitpointTable.waitpointId
    var connectedRuns by TaskRunEntity via WaitpointRunConnectionsTable
    var completedExecutionSnapshots by TaskRunExecutionSnapshotEntity via CompletedWaitpointsTable
    var output by WaitpointTable.output
    var outputType by WaitpointTable.outputType
    var outputIsError by WaitpointTable.outputIsError
    var project by ProjectEntity referencedOn WaitpointTable.projectId
    var projectId by WaitpointTable.projectId
    var environment by RuntimeEnvironmentEntity referencedOn WaitpointTable.environmentId
    var environmentId by WaitpointTable.environmentId
    var createdAt by WaitpointTable.createdAt
    var updatedAt by WaitpointTable.updatedAt
    var tags by WaitpointTable.tags

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(WaitpointTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
