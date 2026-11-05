package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class OauthAccessTokenEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, OauthAccessTokenEntity>(OauthAccessTokenTable)

    var token by OauthAccessTokenTable.token
    var clientId by OauthAccessTokenTable.clientId
    var client by OauthClientEntity referencedOn OauthAccessTokenTable.clientId
    var sessionId by OauthAccessTokenTable.sessionId
    var session by SessionEntity optionalReferencedOn OauthAccessTokenTable.sessionId
    var userId by OauthAccessTokenTable.userId
    var user by UserEntity optionalReferencedOn OauthAccessTokenTable.userId
    var referenceId by OauthAccessTokenTable.referenceId
    var authorizationCodeId by OauthAccessTokenTable.authorizationCodeId
    var resources by OauthAccessTokenTable.resources
    var requestedUserInfoClaims by OauthAccessTokenTable.requestedUserInfoClaims
    var refreshId by OauthAccessTokenTable.refreshId
    var refresh by OauthRefreshTokenEntity optionalReferencedOn OauthAccessTokenTable.refreshId
    var expiresAt by OauthAccessTokenTable.expiresAt
    var createdAt by OauthAccessTokenTable.createdAt
    var revoked by OauthAccessTokenTable.revoked
    var confirmation by OauthAccessTokenTable.confirmation
    var scopes by OauthAccessTokenTable.scopes
}
