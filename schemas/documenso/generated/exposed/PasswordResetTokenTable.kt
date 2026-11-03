package documenso.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object PasswordResetTokenTable : IdTable<Int>("\"PasswordResetToken\"") {
    override val id = integer("id").autoIncrement().entityId()
    val token = text("token").uniqueIndex("\"PasswordResetToken_token_key\"")
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val expiry = pgTimestamp("expiry", 3)
    val userId =
        reference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"PasswordResetToken_userId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"PasswordResetToken_pkey\"")
}
