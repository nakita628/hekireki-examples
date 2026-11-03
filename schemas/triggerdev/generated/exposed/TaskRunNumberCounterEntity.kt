package triggerdev.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class TaskRunNumberCounterEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, TaskRunNumberCounterEntity>(TaskRunNumberCounterTable)

    var taskIdentifier by TaskRunNumberCounterTable.taskIdentifier
    var environment by RuntimeEnvironmentEntity referencedOn TaskRunNumberCounterTable.environmentId
    var environmentId by TaskRunNumberCounterTable.environmentId
    var lastNumber by TaskRunNumberCounterTable.lastNumber
}
