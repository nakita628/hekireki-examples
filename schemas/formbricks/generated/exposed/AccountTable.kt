package formbricks.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object AccountTable : IdTable<String>("\"Account\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val userId =
        reference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Account_userId_fkey\"",
        )
    val type = text("type").nullable()
    val provider = text("provider")
    val providerAccountId = text("providerAccountId")
    val accessToken = text("access_token").nullable()
    val refreshToken = text("refresh_token").nullable()
    val expiresAt = integer("expires_at").nullable()
    val extExpiresIn = integer("ext_expires_in").nullable()
    val tokenType = text("token_type").nullable()
    val scope = text("scope").nullable()
    val idToken = text("id_token").nullable()
    val sessionState = text("session_state").nullable()
    val password = text("password").nullable()
    val accessTokenExpiresAt = pgTimestamp("accessTokenExpiresAt", 3).nullable()
    val refreshTokenExpiresAt = pgTimestamp("refreshTokenExpiresAt", 3).nullable()
    val issuer = text("issuer").nullable()
    override val primaryKey = PrimaryKey(id, name = "\"Account_pkey\"")

    init {
        index("\"Account_userId_idx\"", false, userId)
        uniqueIndex("\"Account_provider_providerAccountId_key\"", provider, providerAccountId)
        uniqueIndex("\"Account_issuer_providerAccountId_key\"", issuer, providerAccountId)
    }
}
