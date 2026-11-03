package formbricks.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb

object OauthRefreshTokenTable : IdTable<String>("\"oauthRefreshToken\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val token = text("token").uniqueIndex("\"oauthRefreshToken_token_key\"")
    val clientId =
        text("clientId").references(
            OauthClientTable.clientId,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"oauthRefreshToken_clientId_fkey\"",
        )
    val sessionId =
        optReference(
            "sessionId",
            SessionTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"oauthRefreshToken_sessionId_fkey\"",
        )
    val userId =
        reference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"oauthRefreshToken_userId_fkey\"",
        )
    val referenceId = text("referenceId").nullable()
    val expiresAt = pgTimestamp("expiresAt", 3)
    val createdAt = pgTimestamp("createdAt", 3)
    val revoked = pgTimestamp("revoked", 3).nullable()
    val authTime = pgTimestamp("authTime", 3).nullable()
    val scopes = array("scopes", TextColumnType()).nullable()
    val authorizationCodeId = text("authorizationCodeId").nullable()
    val resources = array("resources", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val requestedUserInfoClaims = array("requestedUserInfoClaims", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val rotatedAt = pgTimestamp("rotatedAt", 3).nullable()
    val rotationReplayResponse = text("rotationReplayResponse").nullable()
    val rotationReplayExpiresAt = pgTimestamp("rotationReplayExpiresAt", 3).nullable()
    val confirmation = jsonb<String>("confirmation", { it }, { it }).nullable()
    override val primaryKey = PrimaryKey(id, name = "\"oauthRefreshToken_pkey\"")

    init {
        index("\"oauthRefreshToken_clientId_idx\"", false, clientId)
        index("\"oauthRefreshToken_sessionId_idx\"", false, sessionId)
        index("\"oauthRefreshToken_userId_idx\"", false, userId)
        index("\"oauthRefreshToken_authorizationCodeId_idx\"", false, authorizationCodeId)
    }
}
