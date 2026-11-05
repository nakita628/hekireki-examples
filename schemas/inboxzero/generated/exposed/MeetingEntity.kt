package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class MeetingEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, MeetingEntity>(MeetingTable)

    var createdAt by MeetingTable.createdAt
    var updatedAt by MeetingTable.updatedAt
    var calendarEventId by MeetingTable.calendarEventId
    var eventTitle by MeetingTable.eventTitle
    var startTime by MeetingTable.startTime
    var endTime by MeetingTable.endTime
    var attendees by MeetingTable.attendees
    var organizerEmail by MeetingTable.organizerEmail
    var joinOverride by MeetingTable.joinOverride
    var processingStatus by MeetingTable.processingStatus
    var processingAttempts by MeetingTable.processingAttempts
    var summary by MeetingTable.summary
    var followUpDraftStartedAt by MeetingTable.followUpDraftStartedAt
    var followUpDraftId by MeetingTable.followUpDraftId
    var recapSentAt by MeetingTable.recapSentAt
    var recordingId by MeetingTable.recordingId
    var recording by MeetingRecordingEntity optionalReferencedOn MeetingTable.recordingId
    var emailAccountId by MeetingTable.emailAccountId
    var emailAccount by EmailAccountEntity referencedOn MeetingTable.emailAccountId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(MeetingTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
