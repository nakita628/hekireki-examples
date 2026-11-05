package inboxzero.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb

object OauthAccessTokenTable : IdTable<String>("\"OauthAccessToken\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val token = text("token").uniqueIndex("\"OauthAccessToken_token_key\"")
    val clientId =
        text("clientId").references(
            OauthClientTable.clientId,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"OauthAccessToken_clientId_fkey\"",
        )
    val sessionId =
        optReference(
            "sessionId",
            SessionTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"OauthAccessToken_sessionId_fkey\"",
        )
    val userId =
        optReference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"OauthAccessToken_userId_fkey\"",
        )
    val referenceId = text("referenceId").nullable()
    val authorizationCodeId = text("authorizationCodeId").nullable()
    val resources = array("resources", TextColumnType()).nullable()
    val requestedUserInfoClaims = array("requestedUserInfoClaims", TextColumnType()).nullable()
    val refreshId =
        optReference(
            "refreshId",
            OauthRefreshTokenTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"OauthAccessToken_refreshId_fkey\"",
        )
    val expiresAt = pgTimestamp("expiresAt", 3)
    val createdAt = pgTimestamp("createdAt", 3)
    val revoked = pgTimestamp("revoked", 3).nullable()
    val confirmation = jsonb<String>("confirmation", { it }, { it }).nullable()
    val scopes = array("scopes", TextColumnType()).nullable()
    override val primaryKey = PrimaryKey(id, name = "\"OauthAccessToken_pkey\"")

    init {
        index("\"OauthAccessToken_clientId_idx\"", false, clientId)
        index("\"OauthAccessToken_sessionId_idx\"", false, sessionId)
        index("\"OauthAccessToken_userId_idx\"", false, userId)
        index("\"OauthAccessToken_authorizationCodeId_idx\"", false, authorizationCodeId)
        index("\"OauthAccessToken_refreshId_idx\"", false, refreshId)
    }
}
