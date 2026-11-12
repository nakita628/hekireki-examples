package blinko.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class ConfigEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, ConfigEntity>(ConfigTable)

    var key by ConfigTable.key
    var config by ConfigTable.config
    var userId by ConfigTable.userId
    var user by AccountsEntity optionalReferencedOn ConfigTable.userId
}
