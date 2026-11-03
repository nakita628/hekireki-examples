package papermark.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class JacksonIndexEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, JacksonIndexEntity>(JacksonIndexTable)

    var key by JacksonIndexTable.key
    var storeKey by JacksonIndexTable.storeKey
}
