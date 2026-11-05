package inboxzero.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb

object OauthRefreshTokenTable : IdTable<String>("\"OauthRefreshToken\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val token = text("token").uniqueIndex("\"OauthRefreshToken_token_key\"")
    val clientId =
        text("clientId").references(
            OauthClientTable.clientId,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"OauthRefreshToken_clientId_fkey\"",
        )
    val sessionId =
        optReference(
            "sessionId",
            SessionTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"OauthRefreshToken_sessionId_fkey\"",
        )
    val userId =
        reference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"OauthRefreshToken_userId_fkey\"",
        )
    val referenceId = text("referenceId").nullable()
    val authorizationCodeId = text("authorizationCodeId").nullable()
    val resources = array("resources", TextColumnType()).nullable()
    val requestedUserInfoClaims = array("requestedUserInfoClaims", TextColumnType()).nullable()
    val expiresAt = pgTimestamp("expiresAt", 3)
    val createdAt = pgTimestamp("createdAt", 3)
    val revoked = pgTimestamp("revoked", 3).nullable()
    val rotatedAt = pgTimestamp("rotatedAt", 3).nullable()
    val rotationReplayResponse = text("rotationReplayResponse").nullable()
    val rotationReplayExpiresAt = pgTimestamp("rotationReplayExpiresAt", 3).nullable()
    val authTime = pgTimestamp("authTime", 3).nullable()
    val confirmation = jsonb<String>("confirmation", { it }, { it }).nullable()
    val scopes = array("scopes", TextColumnType()).nullable()
    override val primaryKey = PrimaryKey(id, name = "\"OauthRefreshToken_pkey\"")

    init {
        index("\"OauthRefreshToken_clientId_idx\"", false, clientId)
        index("\"OauthRefreshToken_sessionId_idx\"", false, sessionId)
        index("\"OauthRefreshToken_userId_idx\"", false, userId)
        index("\"OauthRefreshToken_authorizationCodeId_idx\"", false, authorizationCodeId)
    }
}
