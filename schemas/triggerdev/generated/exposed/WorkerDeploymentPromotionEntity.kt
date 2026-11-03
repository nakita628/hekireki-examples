package triggerdev.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class WorkerDeploymentPromotionEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, WorkerDeploymentPromotionEntity>(WorkerDeploymentPromotionTable)

    var label by WorkerDeploymentPromotionTable.label
    var deployment by WorkerDeploymentEntity referencedOn WorkerDeploymentPromotionTable.deploymentId
    var deploymentId by WorkerDeploymentPromotionTable.deploymentId
    var environment by RuntimeEnvironmentEntity referencedOn WorkerDeploymentPromotionTable.environmentId
    var environmentId by WorkerDeploymentPromotionTable.environmentId
}
