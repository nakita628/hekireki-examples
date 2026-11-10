package opensaas.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class TaskEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, TaskEntity>(TaskTable)

    var createdAt by TaskTable.createdAt
    var user by UserEntity referencedOn TaskTable.userId
    var userId by TaskTable.userId
    var description by TaskTable.description
    var time by TaskTable.time
    var isDone by TaskTable.isDone
}
