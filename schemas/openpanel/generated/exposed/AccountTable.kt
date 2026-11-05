package openpanel.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object AccountTable : IdTable<String>("accounts") {
    override val id = text("id").databaseDefault("gen_random_uuid()").entityId()
    val userId =
        reference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"accounts_userId_fkey\"",
        )
    val email = text("email").nullable()
    val provider = text("provider")
    val providerId = text("providerId").nullable()
    val accountId = text("accountId").nullable()
    val idToken = text("idToken").nullable()
    val accessToken = text("accessToken").nullable()
    val refreshToken = text("refreshToken").nullable()
    val accessTokenExpiresAt = pgTimestamp("accessTokenExpiresAt", 3).nullable()
    val refreshTokenExpiresAt = pgTimestamp("refreshTokenExpiresAt", 3).nullable()
    val scope = text("scope").nullable()
    val password = text("password").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "accounts_pkey")
}
