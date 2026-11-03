package formbricks.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class OauthRefreshTokenEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, OauthRefreshTokenEntity>(OauthRefreshTokenTable)

    var token by OauthRefreshTokenTable.token
    var clientId by OauthRefreshTokenTable.clientId
    var client by OauthClientEntity referencedOn OauthRefreshTokenTable.clientId
    var sessionId by OauthRefreshTokenTable.sessionId
    var session by SessionEntity optionalReferencedOn OauthRefreshTokenTable.sessionId
    var userId by OauthRefreshTokenTable.userId
    var user by UserEntity referencedOn OauthRefreshTokenTable.userId
    var referenceId by OauthRefreshTokenTable.referenceId
    var expiresAt by OauthRefreshTokenTable.expiresAt
    var createdAt by OauthRefreshTokenTable.createdAt
    var revoked by OauthRefreshTokenTable.revoked
    var authTime by OauthRefreshTokenTable.authTime
    var scopes by OauthRefreshTokenTable.scopes
    val accessTokens by OauthAccessTokenEntity optionalReferrersOn OauthAccessTokenTable.refreshId
    var authorizationCodeId by OauthRefreshTokenTable.authorizationCodeId
    var resources by OauthRefreshTokenTable.resources
    var requestedUserInfoClaims by OauthRefreshTokenTable.requestedUserInfoClaims
    var rotatedAt by OauthRefreshTokenTable.rotatedAt
    var rotationReplayResponse by OauthRefreshTokenTable.rotationReplayResponse
    var rotationReplayExpiresAt by OauthRefreshTokenTable.rotationReplayExpiresAt
    var confirmation by OauthRefreshTokenTable.confirmation
}
