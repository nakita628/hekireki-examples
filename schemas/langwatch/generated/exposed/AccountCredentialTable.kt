package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object AccountCredentialTable : IdTable<String>("\"AccountCredential\"") {
    override val id = text("id").entityId()
    val userId = text("userId")
    val provider = text("provider")
    val password = text("password").nullable()
    val accessToken = text("accessToken").nullable()
    val refreshToken = text("refreshToken").nullable()
    val idToken = text("idToken").nullable()
    val accessTokenExpiresAt = pgTimestamp("accessTokenExpiresAt", 3).nullable()
    val refreshTokenExpiresAt = pgTimestamp("refreshTokenExpiresAt", 3).nullable()
    val scope = text("scope").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"AccountCredential_pkey\"")

    init {
        index("\"AccountCredential_userId_idx\"", false, userId)
    }
}
