package triggerdev.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class WorkerDeploymentEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, WorkerDeploymentEntity>(WorkerDeploymentTable)

    var contentHash by WorkerDeploymentTable.contentHash
    var friendlyId by WorkerDeploymentTable.friendlyId
    var shortCode by WorkerDeploymentTable.shortCode
    var version by WorkerDeploymentTable.version
    var runtime by WorkerDeploymentTable.runtime
    var runtimeVersion by WorkerDeploymentTable.runtimeVersion
    var cliVersion by WorkerDeploymentTable.cliVersion
    var imageReference by WorkerDeploymentTable.imageReference
    var imagePlatform by WorkerDeploymentTable.imagePlatform
    var templateKey by WorkerDeploymentTable.templateKey
    var indexObjectKey by WorkerDeploymentTable.indexObjectKey
    var indexSha256 by WorkerDeploymentTable.indexSha256
    var indexFormatVersion by WorkerDeploymentTable.indexFormatVersion
    var externalBuildData by WorkerDeploymentTable.externalBuildData
    var buildServerMetadata by WorkerDeploymentTable.buildServerMetadata
    var buildEnvVars by WorkerDeploymentTable.buildEnvVars
    var status by WorkerDeploymentTable.status
    var type by WorkerDeploymentTable.type
    var project by ProjectEntity referencedOn WorkerDeploymentTable.projectId
    var projectId by WorkerDeploymentTable.projectId
    var environment by RuntimeEnvironmentEntity referencedOn WorkerDeploymentTable.environmentId
    var environmentId by WorkerDeploymentTable.environmentId
    var worker by BackgroundWorkerEntity optionalReferencedOn WorkerDeploymentTable.workerId
    var workerId by WorkerDeploymentTable.workerId
    var triggeredBy by UserEntity optionalReferencedOn WorkerDeploymentTable.triggeredById
    var triggeredById by WorkerDeploymentTable.triggeredById
    var triggeredVia by WorkerDeploymentTable.triggeredVia
    var commitSHA by WorkerDeploymentTable.commitSHA
    var externalId by WorkerDeploymentTable.externalId
    var startedAt by WorkerDeploymentTable.startedAt
    var installedAt by WorkerDeploymentTable.installedAt
    var builtAt by WorkerDeploymentTable.builtAt
    var deployedAt by WorkerDeploymentTable.deployedAt
    var failedAt by WorkerDeploymentTable.failedAt
    var errorData by WorkerDeploymentTable.errorData
    var canceledAt by WorkerDeploymentTable.canceledAt
    var canceledReason by WorkerDeploymentTable.canceledReason
    var git by WorkerDeploymentTable.git
    var createdAt by WorkerDeploymentTable.createdAt
    var updatedAt by WorkerDeploymentTable.updatedAt
    val promotions by WorkerDeploymentPromotionEntity referrersOn WorkerDeploymentPromotionTable.deploymentId
    val alerts by ProjectAlertEntity optionalReferrersOn ProjectAlertTable.workerDeploymentId
    val workerInstance by WorkerInstanceEntity optionalReferrersOn WorkerInstanceTable.deploymentId
    val integrationDeployments by IntegrationDeploymentEntity optionalReferrersOn IntegrationDeploymentTable.deploymentId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(WorkerDeploymentTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
