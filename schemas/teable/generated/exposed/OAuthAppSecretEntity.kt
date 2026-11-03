package teable.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class OAuthAppSecretEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, OAuthAppSecretEntity>(OAuthAppSecretTable)

    var clientId by OAuthAppSecretTable.clientId
    var secret by OAuthAppSecretTable.secret
    var maskedSecret by OAuthAppSecretTable.maskedSecret
    var createdTime by OAuthAppSecretTable.createdTime
    var createdBy by OAuthAppSecretTable.createdBy
    var lastUsedTime by OAuthAppSecretTable.lastUsedTime
    val tokens by OAuthAppTokenEntity optionalReferrersOn OAuthAppTokenTable.appSecretId
    var oAuthApp by OAuthAppEntity referencedOn OAuthAppSecretTable.clientId
}
