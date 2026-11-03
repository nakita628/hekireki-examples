package triggerdev.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object PersonalAccessTokenTable : IdTable<String>("\"PersonalAccessToken\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val name = text("name")
    val encryptedToken = jsonb<String>("encryptedToken", { it }, { it })
    val obfuscatedToken = text("obfuscatedToken")
    val hashedToken = text("hashedToken").uniqueIndex("\"PersonalAccessToken_hashedToken_key\"")
    val userId =
        reference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"PersonalAccessToken_userId_fkey\"",
        )
    val revokedAt = pgTimestamp("revokedAt", 3).nullable()
    val lastAccessedAt = pgTimestamp("lastAccessedAt", 3).nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"PersonalAccessToken_pkey\"")

    init {
        index("\"PersonalAccessToken_userId_idx\"", false, userId)
    }
}
