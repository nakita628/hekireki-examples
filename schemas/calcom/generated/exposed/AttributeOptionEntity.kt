package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class AttributeOptionEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, AttributeOptionEntity>(AttributeOptionTable)

    var attribute by AttributeEntity referencedOn AttributeOptionTable.attributeId
    var attributeId by AttributeOptionTable.attributeId
    var value by AttributeOptionTable.value
    var slug by AttributeOptionTable.slug
    var isGroup by AttributeOptionTable.isGroup
    var contains by AttributeOptionTable.contains
    val assignedUsers by AttributeToUserEntity referrersOn AttributeToUserTable.attributeOptionId
}
