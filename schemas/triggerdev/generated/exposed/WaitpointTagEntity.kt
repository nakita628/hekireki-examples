package triggerdev.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class WaitpointTagEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, WaitpointTagEntity>(WaitpointTagTable)

    var name by WaitpointTagTable.name
    var environment by RuntimeEnvironmentEntity referencedOn WaitpointTagTable.environmentId
    var environmentId by WaitpointTagTable.environmentId
    var project by ProjectEntity referencedOn WaitpointTagTable.projectId
    var projectId by WaitpointTagTable.projectId
    var createdAt by WaitpointTagTable.createdAt
}
