package rallly.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class ScheduledEventEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ScheduledEventEntity>(ScheduledEventTable)

    var userId by ScheduledEventTable.userId
    var spaceId by ScheduledEventTable.spaceId
    var eventTypeId by ScheduledEventTable.eventTypeId
    var sheetSlotId by ScheduledEventTable.sheetSlotId
    var title by ScheduledEventTable.title
    var description by ScheduledEventTable.description
    var location by ScheduledEventTable.location
    var conferencing by ScheduledEventTable.conferencing
    var capacity by ScheduledEventTable.capacity
    var createdAt by ScheduledEventTable.createdAt
    var updatedAt by ScheduledEventTable.updatedAt
    var status by ScheduledEventTable.status
    var timeZone by ScheduledEventTable.timeZone
    var start by ScheduledEventTable.start
    var end by ScheduledEventTable.end
    var allDay by ScheduledEventTable.allDay
    var deletedAt by ScheduledEventTable.deletedAt
    var sequence by ScheduledEventTable.sequence
    var uid by ScheduledEventTable.uid
    var hideAttendees by ScheduledEventTable.hideAttendees
    var user by UserEntity referencedOn ScheduledEventTable.userId
    var space by SpaceEntity referencedOn ScheduledEventTable.spaceId
    var eventType by EventTypeEntity optionalReferencedOn ScheduledEventTable.eventTypeId
    var sheetSlot by SheetSlotEntity optionalReferencedOn ScheduledEventTable.sheetSlotId
    val rescheduledDates by RescheduledEventDateEntity referrersOn RescheduledEventDateTable.scheduledEventId
    val invites by ScheduledEventInviteEntity referrersOn ScheduledEventInviteTable.scheduledEventId
    val polls by PollEntity optionalReferrersOn PollTable.scheduledEventId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ScheduledEventTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
