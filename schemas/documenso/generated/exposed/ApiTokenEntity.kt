package documenso.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class ApiTokenEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, ApiTokenEntity>(ApiTokenTable)

    var name by ApiTokenTable.name
    var token by ApiTokenTable.token
    var algorithm by ApiTokenTable.algorithm
    var expires by ApiTokenTable.expires
    var createdAt by ApiTokenTable.createdAt
    var lastUsedAt by ApiTokenTable.lastUsedAt
    var userId by ApiTokenTable.userId
    var user by UserEntity optionalReferencedOn ApiTokenTable.userId
    var teamId by ApiTokenTable.teamId
    var team by TeamEntity referencedOn ApiTokenTable.teamId
}
