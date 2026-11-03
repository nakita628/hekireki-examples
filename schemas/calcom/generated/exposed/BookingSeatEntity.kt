package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class BookingSeatEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, BookingSeatEntity>(BookingSeatTable)

    var referenceUid by BookingSeatTable.referenceUid
    var bookingId by BookingSeatTable.bookingId
    var booking by BookingEntity referencedOn BookingSeatTable.bookingId
    var attendeeId by BookingSeatTable.attendeeId
    var attendee by AttendeeEntity referencedOn BookingSeatTable.attendeeId
    var data by BookingSeatTable.data
    var metadata by BookingSeatTable.metadata
}
