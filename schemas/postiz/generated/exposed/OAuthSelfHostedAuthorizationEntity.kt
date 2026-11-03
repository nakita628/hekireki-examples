package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class OAuthSelfHostedAuthorizationEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, OAuthSelfHostedAuthorizationEntity>(OAuthSelfHostedAuthorizationTable)

    var oauthAppId by OAuthSelfHostedAuthorizationTable.oauthAppId
    var mcpUrl by OAuthSelfHostedAuthorizationTable.mcpUrl
    var apiKey by OAuthSelfHostedAuthorizationTable.apiKey
    var email by OAuthSelfHostedAuthorizationTable.email
    var accessToken by OAuthSelfHostedAuthorizationTable.accessToken
    var authorizationCode by OAuthSelfHostedAuthorizationTable.authorizationCode
    var codeExpiresAt by OAuthSelfHostedAuthorizationTable.codeExpiresAt
    var codeChallenge by OAuthSelfHostedAuthorizationTable.codeChallenge
    var codeChallengeMethod by OAuthSelfHostedAuthorizationTable.codeChallengeMethod
    var redirectUri by OAuthSelfHostedAuthorizationTable.redirectUri
    var revokedAt by OAuthSelfHostedAuthorizationTable.revokedAt
    var createdAt by OAuthSelfHostedAuthorizationTable.createdAt
    var updatedAt by OAuthSelfHostedAuthorizationTable.updatedAt
    var oauthApp by OAuthAppEntity referencedOn OAuthSelfHostedAuthorizationTable.oauthAppId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(OAuthSelfHostedAuthorizationTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
