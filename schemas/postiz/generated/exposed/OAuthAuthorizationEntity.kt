package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class OAuthAuthorizationEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, OAuthAuthorizationEntity>(OAuthAuthorizationTable)

    var oauthAppId by OAuthAuthorizationTable.oauthAppId
    var userId by OAuthAuthorizationTable.userId
    var organizationId by OAuthAuthorizationTable.organizationId
    var accessToken by OAuthAuthorizationTable.accessToken
    var authorizationCode by OAuthAuthorizationTable.authorizationCode
    var codeExpiresAt by OAuthAuthorizationTable.codeExpiresAt
    var codeChallenge by OAuthAuthorizationTable.codeChallenge
    var codeChallengeMethod by OAuthAuthorizationTable.codeChallengeMethod
    var redirectUri by OAuthAuthorizationTable.redirectUri
    var revokedAt by OAuthAuthorizationTable.revokedAt
    var createdAt by OAuthAuthorizationTable.createdAt
    var updatedAt by OAuthAuthorizationTable.updatedAt
    var oauthApp by OAuthAppEntity referencedOn OAuthAuthorizationTable.oauthAppId
    var user by UserEntity referencedOn OAuthAuthorizationTable.userId
    var organization by OrganizationEntity referencedOn OAuthAuthorizationTable.organizationId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(OAuthAuthorizationTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
