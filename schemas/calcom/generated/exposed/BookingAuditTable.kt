package calcom.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.core.java.javaUUID
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object BookingAuditTable : IdTable<UUID>("\"BookingAudit\"") {
    override val id = javaUUID("id").clientDefault { uuidV7() }.entityId()
    val bookingUid = text("bookingUid")
    val actorId =
        reference(
            "actorId",
            AuditActorTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"BookingAudit_actorId_fkey\"",
        )
    val type = pgEnum("type", "\"BookingAuditType\"", BookingAuditType.entries, BookingAuditType::dbName)
    val action = pgEnum("action", "\"BookingAuditAction\"", BookingAuditAction.entries, BookingAuditAction::dbName)
    val timestamp = pgTimestamp("timestamp", 3)
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val source1 = pgEnum("source", "\"BookingAuditSource\"", BookingAuditSource.entries, BookingAuditSource::dbName)
    val operationId = text("operationId")
    val data = jsonb<String>("data", { it }, { it }).nullable()
    val context = jsonb<String>("context", { it }, { it }).nullable()
    override val primaryKey = PrimaryKey(id, name = "\"BookingAudit_pkey\"")

    init {
        index("\"BookingAudit_actorId_idx\"", false, actorId)
        index("\"BookingAudit_bookingUid_idx\"", false, bookingUid)
        index("\"BookingAudit_timestamp_idx\"", false, timestamp)
        index("\"BookingAudit_operationId_idx\"", false, operationId)
    }
}
