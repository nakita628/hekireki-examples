package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class OauthClientEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, OauthClientEntity>(OauthClientTable)

    var clientId by OauthClientTable.clientId
    var clientSecret by OauthClientTable.clientSecret
    var clientDiscoveryId by OauthClientTable.clientDiscoveryId
    var disabled by OauthClientTable.disabled
    var skipConsent by OauthClientTable.skipConsent
    var enableEndSession by OauthClientTable.enableEndSession
    var subjectType by OauthClientTable.subjectType
    var scopes by OauthClientTable.scopes
    var clientCredentialsScopes by OauthClientTable.clientCredentialsScopes
    var userId by OauthClientTable.userId
    var user by UserEntity optionalReferencedOn OauthClientTable.userId
    var createdAt by OauthClientTable.createdAt
    var updatedAt by OauthClientTable.updatedAt
    var name by OauthClientTable.name
    var uri by OauthClientTable.uri
    var icon by OauthClientTable.icon
    var contacts by OauthClientTable.contacts
    var tos by OauthClientTable.tos
    var policy by OauthClientTable.policy
    var softwareId by OauthClientTable.softwareId
    var softwareVersion by OauthClientTable.softwareVersion
    var softwareStatement by OauthClientTable.softwareStatement
    var redirectUris by OauthClientTable.redirectUris
    var postLogoutRedirectUris by OauthClientTable.postLogoutRedirectUris
    var backchannelLogoutUri by OauthClientTable.backchannelLogoutUri
    var backchannelLogoutSessionRequired by OauthClientTable.backchannelLogoutSessionRequired
    var tokenEndpointAuthMethod by OauthClientTable.tokenEndpointAuthMethod
    var applicationType by OauthClientTable.applicationType
    var jwks by OauthClientTable.jwks
    var jwksUri by OauthClientTable.jwksUri
    var grantTypes by OauthClientTable.grantTypes
    var responseTypes by OauthClientTable.responseTypes
    var requirePKCE by OauthClientTable.requirePKCE
    var dpopBoundAccessTokens by OauthClientTable.dpopBoundAccessTokens
    var referenceId by OauthClientTable.referenceId
    var metadata by OauthClientTable.metadata
    val oauthClientResources by OauthClientResourceEntity referrersOn OauthClientResourceTable.clientId
    val oauthrefreshtokens by OauthRefreshTokenEntity referrersOn OauthRefreshTokenTable.clientId
    val oauthaccesstokens by OauthAccessTokenEntity referrersOn OauthAccessTokenTable.clientId
    val oauthconsents by OauthConsentEntity referrersOn OauthConsentTable.clientId
}
