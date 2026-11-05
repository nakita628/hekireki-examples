package inboxzero.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object BookingTable : IdTable<String>("\"Booking\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val guestName = text("guestName")
    val guestEmail = text("guestEmail")
    val guestNote = text("guestNote").nullable()
    val startTime = pgTimestamp("startTime", 3)
    val endTime = pgTimestamp("endTime", 3)
    val status =
        pgEnum("status", "\"BookingStatus\"", BookingStatus.entries, BookingStatus::dbName).default(BookingStatus.PENDING_PROVIDER_EVENT)
    val provider = text("provider").nullable()
    val providerConnectionId = text("providerConnectionId").nullable()
    val providerCalendarId = text("providerCalendarId").nullable()
    val providerEventId = text("providerEventId").nullable()
    val videoConferenceLink = text("videoConferenceLink").nullable()
    val cancelTokenHash = text("cancelTokenHash")
    val cancellationReason = text("cancellationReason").nullable()
    val idempotencyToken = text("idempotencyToken").nullable()
    val bookingLinkId =
        reference(
            "bookingLinkId",
            BookingLinkTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Booking_bookingLinkId_fkey\"",
        )
    val emailAccountId =
        reference(
            "emailAccountId",
            EmailAccountTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Booking_emailAccountId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"Booking_pkey\"")

    init {
        index("\"Booking_bookingLinkId_startTime_endTime_idx\"", false, bookingLinkId, startTime, endTime)
        index("\"Booking_emailAccountId_idx\"", false, emailAccountId)
        uniqueIndex("\"Booking_bookingLinkId_idempotencyToken_key\"", bookingLinkId, idempotencyToken)
    }
}
