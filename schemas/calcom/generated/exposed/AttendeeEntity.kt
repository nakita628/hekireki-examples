package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class AttendeeEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, AttendeeEntity>(AttendeeTable)

    var email by AttendeeTable.email
    var name by AttendeeTable.name
    var timeZone by AttendeeTable.timeZone
    var phoneNumber by AttendeeTable.phoneNumber
    var locale by AttendeeTable.locale
    var booking by BookingEntity optionalReferencedOn AttendeeTable.bookingId
    var bookingId by AttendeeTable.bookingId
    val bookingSeat by BookingSeatEntity optionalBackReferencedOn BookingSeatTable.attendeeId
    var noShow by AttendeeTable.noShow
}
