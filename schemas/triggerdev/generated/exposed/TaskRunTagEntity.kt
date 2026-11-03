package triggerdev.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class TaskRunTagEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, TaskRunTagEntity>(TaskRunTagTable)

    var name by TaskRunTagTable.name
    var friendlyId by TaskRunTagTable.friendlyId
    var project by ProjectEntity referencedOn TaskRunTagTable.projectId
    var projectId by TaskRunTagTable.projectId
    var createdAt by TaskRunTagTable.createdAt
}
