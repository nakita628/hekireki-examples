package calcom.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object AssignmentReasonTable : IdTable<Int>("\"AssignmentReason\"") {
    override val id = integer("id").autoIncrement().entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val bookingId =
        reference(
            "bookingId",
            BookingTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"AssignmentReason_bookingId_fkey\"",
        )
    val reasonEnum = pgEnum("reasonEnum", "\"AssignmentReasonEnum\"", AssignmentReasonEnum.entries, AssignmentReasonEnum::dbName)
    val reasonString = text("reasonString")
    override val primaryKey = PrimaryKey(id, name = "\"AssignmentReason_pkey\"")

    init {
        uniqueIndex("\"AssignmentReason_id_key\"", id)
        index("\"AssignmentReason_bookingId_idx\"", false, bookingId)
    }
}
