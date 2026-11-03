package calcom.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb

object BookingSeatTable : IdTable<Int>("\"BookingSeat\"") {
    override val id = integer("id").autoIncrement().entityId()
    val referenceUid = text("referenceUid").uniqueIndex("\"BookingSeat_referenceUid_key\"")
    val bookingId =
        reference(
            "bookingId",
            BookingTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"BookingSeat_bookingId_fkey\"",
        )
    val attendeeId =
        reference(
            "attendeeId",
            AttendeeTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"BookingSeat_attendeeId_fkey\"",
        ).uniqueIndex("\"BookingSeat_attendeeId_key\"")
    val data = jsonb<String>("data", { it }, { it }).nullable()
    val metadata = jsonb<String>("metadata", { it }, { it }).nullable()
    override val primaryKey = PrimaryKey(id, name = "\"BookingSeat_pkey\"")

    init {
        index("\"BookingSeat_bookingId_idx\"", false, bookingId)
        index("\"BookingSeat_attendeeId_idx\"", false, attendeeId)
    }
}
