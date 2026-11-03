package formbricks.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb

object OauthAccessTokenTable : IdTable<String>("\"oauthAccessToken\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val token = text("token").uniqueIndex("\"oauthAccessToken_token_key\"")
    val clientId =
        text("clientId").references(
            OauthClientTable.clientId,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"oauthAccessToken_clientId_fkey\"",
        )
    val sessionId =
        optReference(
            "sessionId",
            SessionTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"oauthAccessToken_sessionId_fkey\"",
        )
    val userId =
        optReference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"oauthAccessToken_userId_fkey\"",
        )
    val referenceId = text("referenceId").nullable()
    val refreshId =
        optReference(
            "refreshId",
            OauthRefreshTokenTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"oauthAccessToken_refreshId_fkey\"",
        )
    val expiresAt = pgTimestamp("expiresAt", 3)
    val createdAt = pgTimestamp("createdAt", 3)
    val scopes = array("scopes", TextColumnType()).nullable()
    val authorizationCodeId = text("authorizationCodeId").nullable()
    val resources = array("resources", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val requestedUserInfoClaims = array("requestedUserInfoClaims", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val revoked = pgTimestamp("revoked", 3).nullable()
    val confirmation = jsonb<String>("confirmation", { it }, { it }).nullable()
    override val primaryKey = PrimaryKey(id, name = "\"oauthAccessToken_pkey\"")

    init {
        index("\"oauthAccessToken_clientId_idx\"", false, clientId)
        index("\"oauthAccessToken_sessionId_idx\"", false, sessionId)
        index("\"oauthAccessToken_userId_idx\"", false, userId)
        index("\"oauthAccessToken_refreshId_idx\"", false, refreshId)
        index("\"oauthAccessToken_authorizationCodeId_idx\"", false, authorizationCodeId)
    }
}
