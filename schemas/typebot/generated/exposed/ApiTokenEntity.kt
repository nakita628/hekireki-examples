package typebot.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class ApiTokenEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ApiTokenEntity>(ApiTokenTable)

    var createdAt by ApiTokenTable.createdAt
    var token by ApiTokenTable.token
    var name by ApiTokenTable.name
    var ownerId by ApiTokenTable.ownerId
    var owner by UserEntity referencedOn ApiTokenTable.ownerId
}
