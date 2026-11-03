package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class TrackingEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, TrackingEntity>(TrackingTable)

    var bookingId by TrackingTable.bookingId
    var booking by BookingEntity referencedOn TrackingTable.bookingId
    var utmSource by TrackingTable.utmSource
    var utmMedium by TrackingTable.utmMedium
    var utmCampaign by TrackingTable.utmCampaign
    var utmTerm by TrackingTable.utmTerm
    var utmContent by TrackingTable.utmContent
}
