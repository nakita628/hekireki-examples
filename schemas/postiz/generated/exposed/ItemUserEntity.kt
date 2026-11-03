package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class ItemUserEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ItemUserEntity>(ItemUserTable)

    var userId by ItemUserTable.userId
    var key by ItemUserTable.key
    var user by UserEntity referencedOn ItemUserTable.userId
}
