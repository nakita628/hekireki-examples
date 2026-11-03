package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class BookingReferenceEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, BookingReferenceEntity>(BookingReferenceTable)

    var type by BookingReferenceTable.type
    var uid by BookingReferenceTable.uid
    var meetingId by BookingReferenceTable.meetingId
    var thirdPartyRecurringEventId by BookingReferenceTable.thirdPartyRecurringEventId
    var meetingPassword by BookingReferenceTable.meetingPassword
    var meetingUrl by BookingReferenceTable.meetingUrl
    var booking by BookingEntity optionalReferencedOn BookingReferenceTable.bookingId
    var bookingId by BookingReferenceTable.bookingId
    var externalCalendarId by BookingReferenceTable.externalCalendarId
    var deleted by BookingReferenceTable.deleted
    var credential by CredentialEntity optionalReferencedOn BookingReferenceTable.credentialId
    var credentialId by BookingReferenceTable.credentialId
    var delegationCredential by DelegationCredentialEntity optionalReferencedOn BookingReferenceTable.delegationCredentialId
    var delegationCredentialId by BookingReferenceTable.delegationCredentialId
}
