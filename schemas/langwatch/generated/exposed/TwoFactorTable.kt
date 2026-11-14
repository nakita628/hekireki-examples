package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object TwoFactorTable : IdTable<String>("\"TwoFactor\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val userId = text("userId")
    val secret = text("secret")
    val backupCodes = text("backupCodes")
    val verified = bool("verified").default(true)
    val failedVerificationCount = integer("failedVerificationCount").default(0)
    val lockedUntil = pgTimestamp("lockedUntil", 3).nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"TwoFactor_pkey\"")

    init {
        index("\"TwoFactor_userId_idx\"", false, userId)
        index("\"TwoFactor_secret_idx\"", false, secret)
    }
}
