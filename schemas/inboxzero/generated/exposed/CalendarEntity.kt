package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class CalendarEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, CalendarEntity>(CalendarTable)

    var createdAt by CalendarTable.createdAt
    var updatedAt by CalendarTable.updatedAt
    var calendarId by CalendarTable.calendarId
    var name by CalendarTable.name
    var description by CalendarTable.description
    var primary by CalendarTable.primary
    var isEnabled by CalendarTable.isEnabled
    var timezone by CalendarTable.timezone
    var connectionId by CalendarTable.connectionId
    var connection by CalendarConnectionEntity referencedOn CalendarTable.connectionId
    val bookingLinks by BookingLinkEntity optionalReferrersOn BookingLinkTable.destinationCalendarId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(CalendarTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
