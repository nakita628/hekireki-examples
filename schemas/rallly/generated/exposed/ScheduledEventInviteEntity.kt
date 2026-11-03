package rallly.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class ScheduledEventInviteEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ScheduledEventInviteEntity>(ScheduledEventInviteTable)

    var uid by ScheduledEventInviteTable.uid
    var scheduledEventId by ScheduledEventInviteTable.scheduledEventId
    var inviteeName by ScheduledEventInviteTable.inviteeName
    var inviteeEmail by ScheduledEventInviteTable.inviteeEmail
    var inviteeId by ScheduledEventInviteTable.inviteeId
    var inviteeTimeZone by ScheduledEventInviteTable.inviteeTimeZone
    var inviteeLocale by ScheduledEventInviteTable.inviteeLocale
    var status by ScheduledEventInviteTable.status
    var createdAt by ScheduledEventInviteTable.createdAt
    var updatedAt by ScheduledEventInviteTable.updatedAt
    var scheduledEvent by ScheduledEventEntity referencedOn ScheduledEventInviteTable.scheduledEventId
    var user by UserEntity optionalReferencedOn ScheduledEventInviteTable.inviteeId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ScheduledEventInviteTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
