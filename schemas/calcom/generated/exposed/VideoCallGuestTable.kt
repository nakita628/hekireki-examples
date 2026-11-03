package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object VideoCallGuestTable : IdTable<String>("\"VideoCallGuest\"") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val bookingUid = text("bookingUid")
    val email = text("email")
    val name = text("name")
    val joinedAt = pgTimestamp("joinedAt", 3).nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"VideoCallGuest_pkey\"")

    init {
        index("\"VideoCallGuest_bookingUid_idx\"", false, bookingUid)
        index("\"VideoCallGuest_email_idx\"", false, email)
        uniqueIndex("\"VideoCallGuest_bookingUid_email_key\"", bookingUid, email)
    }
}
