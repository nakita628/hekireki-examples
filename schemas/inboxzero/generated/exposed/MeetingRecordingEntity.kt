package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class MeetingRecordingEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, MeetingRecordingEntity>(MeetingRecordingTable)

    var createdAt by MeetingRecordingTable.createdAt
    var updatedAt by MeetingRecordingTable.updatedAt
    var botProvider by MeetingRecordingTable.botProvider
    var externalBotId by MeetingRecordingTable.externalBotId
    var botName by MeetingRecordingTable.botName
    var externalRecordingId by MeetingRecordingTable.externalRecordingId
    var transcriptRequestedAt by MeetingRecordingTable.transcriptRequestedAt
    var externalTranscriptId by MeetingRecordingTable.externalTranscriptId
    var meetingUrl by MeetingRecordingTable.meetingUrl
    var normalizedMeetingUrl by MeetingRecordingTable.normalizedMeetingUrl
    var activeKey by MeetingRecordingTable.activeKey
    var meetingStartTime by MeetingRecordingTable.meetingStartTime
    var status by MeetingRecordingTable.status
    var failureReason by MeetingRecordingTable.failureReason
    var transcript by MeetingRecordingTable.transcript
    var transcriptFetchedAt by MeetingRecordingTable.transcriptFetchedAt
    var mediaDeletedAt by MeetingRecordingTable.mediaDeletedAt
    var emailAccountId by MeetingRecordingTable.emailAccountId
    var emailAccount by EmailAccountEntity referencedOn MeetingRecordingTable.emailAccountId
    val meetings by MeetingEntity optionalReferrersOn MeetingTable.recordingId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(MeetingRecordingTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
