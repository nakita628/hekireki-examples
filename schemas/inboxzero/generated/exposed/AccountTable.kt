package inboxzero.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object AccountTable : IdTable<String>("\"Account\"") {
    val password = text("password").nullable()
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val userId =
        reference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Account_userId_fkey\"",
        )
    val provider = text("provider")
    val type = text("type").default("oidc")
    val providerAccountId = text("providerAccountId")
    val refreshToken = text("refresh_token").nullable()
    val refreshTokenExpiresAt = pgTimestamp("refreshTokenExpiresAt", 3).nullable()
    val accessToken = text("access_token").nullable()
    val expiresAt =
        pgTimestamp("expires_at", 3).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val disconnectedAt = pgTimestamp("disconnectedAt", 3).nullable()
    val tokenType = text("token_type").nullable()
    val scope = text("scope").nullable()
    val idToken = text("id_token").nullable()
    val sessionState = text("session_state").nullable()
    override val primaryKey = PrimaryKey(id, name = "\"Account_pkey\"")

    init {
        index("\"Account_userId_idx\"", false, userId)
        uniqueIndex("\"Account_provider_providerAccountId_key\"", provider, providerAccountId)
    }
}
