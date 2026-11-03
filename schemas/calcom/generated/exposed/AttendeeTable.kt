package calcom.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable

object AttendeeTable : IdTable<Int>("\"Attendee\"") {
    override val id = integer("id").autoIncrement().entityId()
    val email = text("email")
    val name = text("name")
    val timeZone = text("timeZone")
    val phoneNumber = text("phoneNumber").nullable()
    val locale = text("locale").nullable().default("en")
    val bookingId =
        optReference(
            "bookingId",
            BookingTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Attendee_bookingId_fkey\"",
        )
    val noShow = bool("noShow").nullable().default(false)
    override val primaryKey = PrimaryKey(id, name = "\"Attendee_pkey\"")

    init {
        index("\"Attendee_email_idx\"", false, email)
        index("\"Attendee_bookingId_idx\"", false, bookingId)
        index("\"Attendee_email_bookingId_idx\"", false, email, bookingId)
    }
}
