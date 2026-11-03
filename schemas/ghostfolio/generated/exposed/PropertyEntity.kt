package ghostfolio.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class PropertyEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, PropertyEntity>(PropertyTable)

    var value by PropertyTable.value
}
