package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class BookingEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, BookingEntity>(BookingTable)

    var createdAt by BookingTable.createdAt
    var updatedAt by BookingTable.updatedAt
    var guestName by BookingTable.guestName
    var guestEmail by BookingTable.guestEmail
    var guestNote by BookingTable.guestNote
    var startTime by BookingTable.startTime
    var endTime by BookingTable.endTime
    var status by BookingTable.status
    var provider by BookingTable.provider
    var providerConnectionId by BookingTable.providerConnectionId
    var providerCalendarId by BookingTable.providerCalendarId
    var providerEventId by BookingTable.providerEventId
    var videoConferenceLink by BookingTable.videoConferenceLink
    var cancelTokenHash by BookingTable.cancelTokenHash
    var cancellationReason by BookingTable.cancellationReason
    var idempotencyToken by BookingTable.idempotencyToken
    var bookingLinkId by BookingTable.bookingLinkId
    var bookingLink by BookingLinkEntity referencedOn BookingTable.bookingLinkId
    var emailAccountId by BookingTable.emailAccountId
    var emailAccount by EmailAccountEntity referencedOn BookingTable.emailAccountId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(BookingTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
