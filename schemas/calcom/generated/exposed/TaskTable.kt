package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object TaskTable : IdTable<String>("\"Task\"") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val scheduledAt = pgTimestamp("scheduledAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val succeededAt = pgTimestamp("succeededAt", 3).nullable()
    val type = text("type")
    val payload = text("payload")
    val attempts = integer("attempts").default(0)
    val maxAttempts = integer("maxAttempts").default(3)
    val lastError = text("lastError").nullable()
    val lastFailedAttemptAt = pgTimestamp("lastFailedAttemptAt", 3).nullable()
    val referenceUid = text("referenceUid").nullable()
    override val primaryKey = PrimaryKey(id, name = "\"Task_pkey\"")

    init {
        uniqueIndex("\"Task_id_key\"", id)
        index("\"Task_succeededAt_idx\"", false, succeededAt)
        index("\"Task_scheduledAt_succeededAt_idx\"", false, scheduledAt, succeededAt)
        uniqueIndex("\"Task_referenceUid_type_key\"", referenceUid, type)
    }
}
