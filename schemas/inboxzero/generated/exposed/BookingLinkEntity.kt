package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class BookingLinkEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, BookingLinkEntity>(BookingLinkTable)

    var createdAt by BookingLinkTable.createdAt
    var updatedAt by BookingLinkTable.updatedAt
    var slug by BookingLinkTable.slug
    var title by BookingLinkTable.title
    var description by BookingLinkTable.description
    var isActive by BookingLinkTable.isActive
    var durationMinutes by BookingLinkTable.durationMinutes
    var slotIntervalMinutes by BookingLinkTable.slotIntervalMinutes
    var locationType by BookingLinkTable.locationType
    var locationValue by BookingLinkTable.locationValue
    var minimumNoticeMinutes by BookingLinkTable.minimumNoticeMinutes
    var maxDaysAhead by BookingLinkTable.maxDaysAhead
    var emailAccountId by BookingLinkTable.emailAccountId
    var emailAccount by EmailAccountEntity referencedOn BookingLinkTable.emailAccountId
    var destinationCalendarId by BookingLinkTable.destinationCalendarId
    var destinationCalendar by CalendarEntity optionalReferencedOn BookingLinkTable.destinationCalendarId
    var availabilityScheduleId by BookingLinkTable.availabilityScheduleId
    val bookings by BookingEntity referrersOn BookingTable.bookingLinkId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(BookingLinkTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
