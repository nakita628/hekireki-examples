package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class OrganizationSeatPeakEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, OrganizationSeatPeakEntity>(OrganizationSeatPeakTable)

    var openedAt by OrganizationSeatPeakTable.openedAt
    var fullSeats by OrganizationSeatPeakTable.fullSeats
    var liteSeats by OrganizationSeatPeakTable.liteSeats
    var developerSeats by OrganizationSeatPeakTable.developerSeats
    var latestChangeAt by OrganizationSeatPeakTable.latestChangeAt
    var mismatches by OrganizationSeatPeakTable.mismatches
}
