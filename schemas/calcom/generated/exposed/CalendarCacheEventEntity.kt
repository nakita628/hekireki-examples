package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class CalendarCacheEventEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, CalendarCacheEventEntity>(CalendarCacheEventTable)

    var selectedCalendarId by CalendarCacheEventTable.selectedCalendarId
    var externalId by CalendarCacheEventTable.externalId
    var externalEtag by CalendarCacheEventTable.externalEtag
    var iCalUID by CalendarCacheEventTable.iCalUID
    var iCalSequence by CalendarCacheEventTable.iCalSequence
    var summary by CalendarCacheEventTable.summary
    var description by CalendarCacheEventTable.description
    var location by CalendarCacheEventTable.location
    var start by CalendarCacheEventTable.start
    var end by CalendarCacheEventTable.end
    var isAllDay by CalendarCacheEventTable.isAllDay
    var timeZone by CalendarCacheEventTable.timeZone
    var status by CalendarCacheEventTable.status
    var recurringEventId by CalendarCacheEventTable.recurringEventId
    var originalStartTime by CalendarCacheEventTable.originalStartTime
    var createdAt by CalendarCacheEventTable.createdAt
    var updatedAt by CalendarCacheEventTable.updatedAt
    var externalCreatedAt by CalendarCacheEventTable.externalCreatedAt
    var externalUpdatedAt by CalendarCacheEventTable.externalUpdatedAt
    var selectedCalendar by SelectedCalendarEntity referencedOn CalendarCacheEventTable.selectedCalendarId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(CalendarCacheEventTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
