package rallly.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object AccountTable : IdTable<String>("accounts") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val userId =
        reference(
            "user_id",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "accounts_user_id_fkey",
        )
    val provider = text("provider")
    val providerAccountId = text("provider_account_id")
    val refreshToken = text("refresh_token").nullable()
    val accessToken = text("access_token").nullable()
    val expiresAt = integer("expires_at").nullable()
    val scope = text("scope").nullable()
    val idToken = text("id_token").nullable()
    val accessTokenExpiresAt = pgTimestamp("access_token_expires_at", 3).nullable()
    val refreshTokenExpiresAt = pgTimestamp("refresh_token_expires_at", 3).nullable()
    val password = text("password").nullable()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "accounts_pkey")

    init {
        index("accounts_user_id_idx", false, userId)
        uniqueIndex("accounts_provider_provider_account_id_key", provider, providerAccountId)
    }
}
