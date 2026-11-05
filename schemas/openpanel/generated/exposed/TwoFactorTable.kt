package openpanel.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object TwoFactorTable : IdTable<String>("two_factor") {
    override val id = text("id").databaseDefault("gen_random_uuid()").entityId()
    val userId =
        reference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"two_factor_userId_fkey\"",
        ).uniqueIndex("\"two_factor_userId_key\"")
    val secret = text("secret")
    val backupCodes = text("backupCodes")
    val verified = bool("verified").default(true)
    val failedVerificationCount = integer("failedVerificationCount").default(0)
    val lockedUntil = pgTimestamp("lockedUntil", 3).nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "two_factor_pkey")
}
