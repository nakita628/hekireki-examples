package postiz.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object OAuthSelfHostedAuthorizationTable : IdTable<String>("\"OAuthSelfHostedAuthorization\"") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val oauthAppId =
        reference(
            "oauthAppId",
            OAuthAppTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"OAuthSelfHostedAuthorization_oauthAppId_fkey\"",
        )
    val mcpUrl = text("mcpUrl")
    val apiKey = text("apiKey")
    val email = text("email").nullable()
    val accessToken = text("accessToken").nullable()
    val authorizationCode = text("authorizationCode").nullable()
    val codeExpiresAt = pgTimestamp("codeExpiresAt", 3).nullable()
    val codeChallenge = text("codeChallenge").nullable()
    val codeChallengeMethod = text("codeChallengeMethod").nullable()
    val redirectUri = text("redirectUri").nullable()
    val revokedAt = pgTimestamp("revokedAt", 3).nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"OAuthSelfHostedAuthorization_pkey\"")

    init {
        index("\"OAuthSelfHostedAuthorization_accessToken_idx\"", false, accessToken)
        index("\"OAuthSelfHostedAuthorization_authorizationCode_idx\"", false, authorizationCode)
        index("\"OAuthSelfHostedAuthorization_oauthAppId_idx\"", false, oauthAppId)
    }
}
