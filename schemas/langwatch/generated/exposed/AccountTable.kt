package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object AccountTable : IdTable<String>("\"Account\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val userId = text("userId")
    val type = text("type").default("oauth")
    val provider = text("provider")
    val issuer = text("issuer").nullable()
    val providerAccountId = text("providerAccountId")
    val refreshToken = text("refresh_token").nullable()
    val accessToken = text("access_token").nullable()
    val expiresAt = pgTimestamp("expires_at", 3).nullable()
    val extExpiresIn = integer("ext_expires_in").nullable()
    val tokenType = text("token_type").nullable()
    val scope = text("scope").nullable()
    val idToken = text("id_token").nullable()
    val sessionState = text("session_state").nullable()
    val password = text("password").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"Account_pkey\"")

    init {
        index("\"Account_userId_idx\"", false, userId)
        index("\"Account_issuer_providerAccountId_idx\"", false, issuer, providerAccountId)
        uniqueIndex("\"Account_provider_providerAccountId_key\"", provider, providerAccountId)
    }
}
