package formbricks.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object PasswordResetTokenTable : IdTable<String>("\"PasswordResetToken\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val tokenHash = text("token_hash").uniqueIndex("\"PasswordResetToken_token_hash_key\"")
    val expiresAt = pgTimestamp("expires_at", 3)
    val userId =
        reference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"PasswordResetToken_userId_fkey\"",
        ).uniqueIndex("\"PasswordResetToken_userId_key\"")
    override val primaryKey = PrimaryKey(id, name = "\"PasswordResetToken_pkey\"")

    init {
        index("\"PasswordResetToken_expires_at_idx\"", false, expiresAt)
    }
}
