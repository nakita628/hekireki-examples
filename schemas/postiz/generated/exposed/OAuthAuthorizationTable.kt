package postiz.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object OAuthAuthorizationTable : IdTable<String>("\"OAuthAuthorization\"") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val oauthAppId =
        reference(
            "oauthAppId",
            OAuthAppTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"OAuthAuthorization_oauthAppId_fkey\"",
        )
    val userId =
        reference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"OAuthAuthorization_userId_fkey\"",
        )
    val organizationId =
        reference(
            "organizationId",
            OrganizationTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"OAuthAuthorization_organizationId_fkey\"",
        )
    val accessToken = text("accessToken").nullable()
    val authorizationCode = text("authorizationCode").nullable()
    val codeExpiresAt = pgTimestamp("codeExpiresAt", 3).nullable()
    val codeChallenge = text("codeChallenge").nullable()
    val codeChallengeMethod = text("codeChallengeMethod").nullable()
    val redirectUri = text("redirectUri").nullable()
    val revokedAt = pgTimestamp("revokedAt", 3).nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"OAuthAuthorization_pkey\"")

    init {
        index("\"OAuthAuthorization_accessToken_idx\"", false, accessToken)
        index("\"OAuthAuthorization_authorizationCode_idx\"", false, authorizationCode)
        index("\"OAuthAuthorization_oauthAppId_idx\"", false, oauthAppId)
        index("\"OAuthAuthorization_userId_idx\"", false, userId)
        index("\"OAuthAuthorization_organizationId_idx\"", false, organizationId)
        index("\"OAuthAuthorization_revokedAt_idx\"", false, revokedAt)
        uniqueIndex("\"OAuthAuthorization_oauthAppId_userId_organizationId_key\"", oauthAppId, userId, organizationId)
    }
}
