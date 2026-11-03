package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class PlatformAuthorizationTokenEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, PlatformAuthorizationTokenEntity>(PlatformAuthorizationTokenTable)

    var owner by UserEntity referencedOn PlatformAuthorizationTokenTable.userId
    var client by PlatformOAuthClientEntity referencedOn PlatformAuthorizationTokenTable.platformOAuthClientId
    var platformOAuthClientId by PlatformAuthorizationTokenTable.platformOAuthClientId
    var userId by PlatformAuthorizationTokenTable.userId
    var createdAt by PlatformAuthorizationTokenTable.createdAt
}
