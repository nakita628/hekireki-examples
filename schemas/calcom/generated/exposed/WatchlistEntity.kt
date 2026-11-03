package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass
import java.util.UUID

class WatchlistEntity(
    id: EntityID<UUID>,
) : Entity<UUID>(id) {
    companion object : EntityClass<UUID, WatchlistEntity>(WatchlistTable)

    var type by WatchlistTable.type
    var value by WatchlistTable.value
    var description by WatchlistTable.description
    var isGlobal by WatchlistTable.isGlobal
    var organizationId by WatchlistTable.organizationId
    var action by WatchlistTable.action
    var source by WatchlistTable.source1
    var lastUpdatedAt by WatchlistTable.lastUpdatedAt
    val bookingReports by BookingReportEntity optionalReferrersOn BookingReportTable.watchlistId
    val globalBookingReports by BookingReportEntity optionalReferrersOn BookingReportTable.globalWatchlistId
    val audits by WatchlistAuditEntity optionalReferrersOn WatchlistAuditTable.watchlistId
}
