package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object SignInAttemptLockTable : IdTable<String>("\"SignInAttemptLock\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val identifierHash = text("identifierHash").uniqueIndex("\"SignInAttemptLock_identifierHash_key\"")
    val failedCount = integer("failedCount").default(0)
    val lockedUntil = pgTimestamp("lockedUntil", 3).nullable()
    val consecutiveLockouts = integer("consecutiveLockouts").default(0)
    val heldForReview = bool("heldForReview").default(false)
    val userId = text("userId").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"SignInAttemptLock_pkey\"")

    init {
        index("\"SignInAttemptLock_lockedUntil_idx\"", false, lockedUntil)
        index("\"SignInAttemptLock_userId_idx\"", false, userId)
    }
}
