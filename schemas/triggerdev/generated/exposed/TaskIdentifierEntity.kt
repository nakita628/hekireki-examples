package triggerdev.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class TaskIdentifierEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, TaskIdentifierEntity>(TaskIdentifierTable)

    var runtimeEnvironment by RuntimeEnvironmentEntity referencedOn TaskIdentifierTable.runtimeEnvironmentId
    var runtimeEnvironmentId by TaskIdentifierTable.runtimeEnvironmentId
    var project by ProjectEntity referencedOn TaskIdentifierTable.projectId
    var projectId by TaskIdentifierTable.projectId
    var slug by TaskIdentifierTable.slug
    var currentTriggerSource by TaskIdentifierTable.currentTriggerSource
    var currentWorker by BackgroundWorkerEntity optionalReferencedOn TaskIdentifierTable.currentWorkerId
    var currentWorkerId by TaskIdentifierTable.currentWorkerId
    var firstSeenAt by TaskIdentifierTable.firstSeenAt
    var lastSeenAt by TaskIdentifierTable.lastSeenAt
    var isInLatestDeployment by TaskIdentifierTable.isInLatestDeployment
}
