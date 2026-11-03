package triggerdev.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class BackgroundWorkerEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, BackgroundWorkerEntity>(BackgroundWorkerTable)

    var friendlyId by BackgroundWorkerTable.friendlyId
    var engine by BackgroundWorkerTable.engine
    var contentHash by BackgroundWorkerTable.contentHash
    var sdkVersion by BackgroundWorkerTable.sdkVersion
    var cliVersion by BackgroundWorkerTable.cliVersion
    var project by ProjectEntity referencedOn BackgroundWorkerTable.projectId
    var projectId by BackgroundWorkerTable.projectId
    var runtimeEnvironment by RuntimeEnvironmentEntity referencedOn BackgroundWorkerTable.runtimeEnvironmentId
    var runtimeEnvironmentId by BackgroundWorkerTable.runtimeEnvironmentId
    var version by BackgroundWorkerTable.version
    var metadata by BackgroundWorkerTable.metadata
    var runtime by BackgroundWorkerTable.runtime
    var runtimeVersion by BackgroundWorkerTable.runtimeVersion
    var createdAt by BackgroundWorkerTable.createdAt
    var updatedAt by BackgroundWorkerTable.updatedAt
    val tasks by BackgroundWorkerTaskEntity referrersOn BackgroundWorkerTaskTable.workerId
    val attempts by TaskRunAttemptEntity referrersOn TaskRunAttemptTable.backgroundWorkerId
    val lockedRuns by TaskRunEntity optionalReferrersOn TaskRunTable.lockedToVersionId
    var files by BackgroundWorkerFileEntity via BackgroundWorkerToBackgroundWorkerFileTable
    var queues by TaskQueueEntity via BackgroundWorkerToTaskQueueTable
    val promptVersions by PromptVersionEntity optionalReferrersOn PromptVersionTable.workerId
    val deployment by WorkerDeploymentEntity optionalBackReferencedOn WorkerDeploymentTable.workerId
    var workerGroup by WorkerInstanceGroupEntity optionalReferencedOn BackgroundWorkerTable.workerGroupId
    var workerGroupId by BackgroundWorkerTable.workerGroupId
    var supportsLazyAttempts by BackgroundWorkerTable.supportsLazyAttempts
    val taskIdentifiers by TaskIdentifierEntity optionalReferrersOn TaskIdentifierTable.currentWorkerId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(BackgroundWorkerTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
