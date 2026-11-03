package calcom.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object RefreshTokenTable : IdTable<Int>("\"RefreshToken\"") {
    override val id = integer("id").autoIncrement().entityId()
    val secret = text("secret").uniqueIndex("\"RefreshToken_secret_key\"")
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val expiresAt = pgTimestamp("expiresAt", 3)
    val platformOAuthClientId =
        reference(
            "platformOAuthClientId",
            PlatformOAuthClientTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"RefreshToken_platformOAuthClientId_fkey\"",
        )
    val userId =
        reference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"RefreshToken_userId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"RefreshToken_pkey\"")
}
