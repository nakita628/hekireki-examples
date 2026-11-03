package linkwarden.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object AccessTokenTable : IdTable<Int>("\"AccessToken\"") {
    override val id = integer("id").autoIncrement().entityId()
    val name = text("name")
    val userId =
        reference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"AccessToken_userId_fkey\"",
        )
    val token = text("token").uniqueIndex("\"AccessToken_token_key\"")
    val revoked = bool("revoked").default(false)
    val isSession = bool("isSession").default(false)
    val expires = pgTimestamp("expires", 3)
    val lastUsedAt = pgTimestamp("lastUsedAt", 3).nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"AccessToken_pkey\"")
}
