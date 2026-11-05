package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class OauthResourceEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, OauthResourceEntity>(OauthResourceTable)

    var identifier by OauthResourceTable.identifier
    var name by OauthResourceTable.name
    var accessTokenTtl by OauthResourceTable.accessTokenTtl
    var refreshTokenTtl by OauthResourceTable.refreshTokenTtl
    var signingAlgorithm by OauthResourceTable.signingAlgorithm
    var signingKeyId by OauthResourceTable.signingKeyId
    var allowedScopes by OauthResourceTable.allowedScopes
    var customClaims by OauthResourceTable.customClaims
    var dpopBoundAccessTokensRequired by OauthResourceTable.dpopBoundAccessTokensRequired
    var disabled by OauthResourceTable.disabled
    var createdAt by OauthResourceTable.createdAt
    var updatedAt by OauthResourceTable.updatedAt
    var policyVersion by OauthResourceTable.policyVersion
    var metadata by OauthResourceTable.metadata
    val oauthClientResources by OauthClientResourceEntity referrersOn OauthClientResourceTable.resourceId
}
