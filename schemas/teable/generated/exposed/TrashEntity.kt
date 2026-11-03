package teable.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class TrashEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, TrashEntity>(TrashTable)

    var resourceType by TrashTable.resourceType
    var resourceId by TrashTable.resourceId
    var parentId by TrashTable.parentId
    var deletedTime by TrashTable.deletedTime
    var deletedBy by TrashTable.deletedBy
}
