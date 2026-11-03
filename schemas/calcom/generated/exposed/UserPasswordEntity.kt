package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class UserPasswordEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, UserPasswordEntity>(UserPasswordTable)

    var hash by UserPasswordTable.hash
    val user by UserEntity referencedOn UserPasswordTable.id
}
