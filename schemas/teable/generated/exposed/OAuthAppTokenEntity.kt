package teable.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class OAuthAppTokenEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, OAuthAppTokenEntity>(OAuthAppTokenTable)

    var clientId by OAuthAppTokenTable.clientId
    var appSecretId by OAuthAppTokenTable.appSecretId
    var refreshTokenSign by OAuthAppTokenTable.refreshTokenSign
    var expiredTime by OAuthAppTokenTable.expiredTime
    var createdTime by OAuthAppTokenTable.createdTime
    var createdBy by OAuthAppTokenTable.createdBy
    var oAuthAppSecret by OAuthAppSecretEntity optionalReferencedOn OAuthAppTokenTable.appSecretId
    var oAuthApp by OAuthAppEntity referencedOn OAuthAppTokenTable.clientId
}
