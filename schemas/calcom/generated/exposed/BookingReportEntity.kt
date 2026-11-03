package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

class BookingReportEntity(
    id: EntityID<UUID>,
) : Entity<UUID>(id) {
    companion object : EntityClass<UUID, BookingReportEntity>(BookingReportTable)

    var bookingUid by BookingReportTable.bookingUid
    var booking by BookingEntity referencedOn BookingReportTable.bookingUid
    var bookerEmail by BookingReportTable.bookerEmail
    var reportedById by BookingReportTable.reportedById
    var organizationId by BookingReportTable.organizationId
    var reason by BookingReportTable.reason
    var description by BookingReportTable.description
    var cancelled by BookingReportTable.cancelled
    var createdAt by BookingReportTable.createdAt
    var updatedAt by BookingReportTable.updatedAt
    var status by BookingReportTable.status
    var systemStatus by BookingReportTable.systemStatus
    var watchlistId by BookingReportTable.watchlistId
    var globalWatchlistId by BookingReportTable.globalWatchlistId
    var reportedBy by UserEntity optionalReferencedOn BookingReportTable.reportedById
    var organization by TeamEntity optionalReferencedOn BookingReportTable.organizationId
    var watchlist by WatchlistEntity optionalReferencedOn BookingReportTable.watchlistId
    var globalWatchlist by WatchlistEntity optionalReferencedOn BookingReportTable.globalWatchlistId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(BookingReportTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
