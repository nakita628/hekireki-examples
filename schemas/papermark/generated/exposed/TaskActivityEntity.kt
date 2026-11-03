package papermark.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class TaskActivityEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, TaskActivityEntity>(TaskActivityTable)

    var taskId by TaskActivityTable.taskId
    var task by TaskEntity referencedOn TaskActivityTable.taskId
    var type by TaskActivityTable.type
    var comment by TaskActivityTable.comment
    var fromStatus by TaskActivityTable.fromStatus
    var toStatus by TaskActivityTable.toStatus
    var userId by TaskActivityTable.userId
    var user by UserEntity optionalReferencedOn TaskActivityTable.userId
    var viewerId by TaskActivityTable.viewerId
    var viewer by ViewerEntity optionalReferencedOn TaskActivityTable.viewerId
    var viewId by TaskActivityTable.viewId
    var view by ViewEntity optionalReferencedOn TaskActivityTable.viewId
    var createdAt by TaskActivityTable.createdAt
}
