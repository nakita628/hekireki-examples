package teable.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class ShortLinkEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ShortLinkEntity>(ShortLinkTable)

    var code by ShortLinkTable.code
    var type by ShortLinkTable.type
    var resourceId by ShortLinkTable.resourceId
    var createdTime by ShortLinkTable.createdTime
    var createdBy by ShortLinkTable.createdBy
    var deletedTime by ShortLinkTable.deletedTime
}
