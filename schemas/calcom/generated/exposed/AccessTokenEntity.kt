package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class AccessTokenEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, AccessTokenEntity>(AccessTokenTable)

    var secret by AccessTokenTable.secret
    var createdAt by AccessTokenTable.createdAt
    var expiresAt by AccessTokenTable.expiresAt
    var owner by UserEntity referencedOn AccessTokenTable.userId
    var client by PlatformOAuthClientEntity referencedOn AccessTokenTable.platformOAuthClientId
    var platformOAuthClientId by AccessTokenTable.platformOAuthClientId
    var userId by AccessTokenTable.userId
}
