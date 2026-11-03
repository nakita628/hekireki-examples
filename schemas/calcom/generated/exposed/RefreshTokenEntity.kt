package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class RefreshTokenEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, RefreshTokenEntity>(RefreshTokenTable)

    var secret by RefreshTokenTable.secret
    var createdAt by RefreshTokenTable.createdAt
    var expiresAt by RefreshTokenTable.expiresAt
    var owner by UserEntity referencedOn RefreshTokenTable.userId
    var client by PlatformOAuthClientEntity referencedOn RefreshTokenTable.platformOAuthClientId
    var platformOAuthClientId by RefreshTokenTable.platformOAuthClientId
    var userId by RefreshTokenTable.userId
}
