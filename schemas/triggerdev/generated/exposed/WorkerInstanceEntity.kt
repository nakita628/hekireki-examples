package triggerdev.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class WorkerInstanceEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, WorkerInstanceEntity>(WorkerInstanceTable)

    var name by WorkerInstanceTable.name
    var resourceIdentifier by WorkerInstanceTable.resourceIdentifier
    var metadata by WorkerInstanceTable.metadata
    var workerGroup by WorkerInstanceGroupEntity referencedOn WorkerInstanceTable.workerGroupId
    var workerGroupId by WorkerInstanceTable.workerGroupId
    var organization by OrganizationEntity optionalReferencedOn WorkerInstanceTable.organizationId
    var organizationId by WorkerInstanceTable.organizationId
    var project by ProjectEntity optionalReferencedOn WorkerInstanceTable.projectId
    var projectId by WorkerInstanceTable.projectId
    var environment by RuntimeEnvironmentEntity optionalReferencedOn WorkerInstanceTable.environmentId
    var environmentId by WorkerInstanceTable.environmentId
    var deployment by WorkerDeploymentEntity optionalReferencedOn WorkerInstanceTable.deploymentId
    var deploymentId by WorkerInstanceTable.deploymentId
    var createdAt by WorkerInstanceTable.createdAt
    var updatedAt by WorkerInstanceTable.updatedAt
    var lastDequeueAt by WorkerInstanceTable.lastDequeueAt
    var lastHeartbeatAt by WorkerInstanceTable.lastHeartbeatAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(WorkerInstanceTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
