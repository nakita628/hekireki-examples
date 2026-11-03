package teable.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class PinResourceEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, PinResourceEntity>(PinResourceTable)

    var type by PinResourceTable.type
    var resourceId by PinResourceTable.resourceId
    var createdTime by PinResourceTable.createdTime
    var createdBy by PinResourceTable.createdBy
    var order by PinResourceTable.order
}
