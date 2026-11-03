package formbricks.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object AuthzedProjectionOutboxTable : IdTable<String>("\"AuthzedProjectionOutbox\"") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val targetType = text("targetType")
    val primaryId = text("primaryId")
    val secondaryId = text("secondaryId").nullable()
    val isRevocation = bool("isRevocation").default(false)
    val attempts = integer("attempts").default(0)
    val permanentFailures = integer("permanentFailures").default(0)
    val availableAt = pgTimestamp("availableAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val leasedAt = pgTimestamp("leasedAt", 3).nullable()
    val leaseExpiresAt = pgTimestamp("leaseExpiresAt", 3).nullable()
    val leaseOwner = text("leaseOwner").nullable()
    val processedAt = pgTimestamp("processedAt", 3).nullable()
    val deadLetteredAt = pgTimestamp("deadLetteredAt", 3).nullable()
    val lastAttemptAt = pgTimestamp("lastAttemptAt", 3).nullable()
    val lastErrorCode = text("lastErrorCode").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"AuthzedProjectionOutbox_pkey\"")
}
