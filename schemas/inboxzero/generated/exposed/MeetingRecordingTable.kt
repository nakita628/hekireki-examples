package inboxzero.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object MeetingRecordingTable : IdTable<String>("\"MeetingRecording\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val botProvider = text("botProvider").default("recall")
    val externalBotId = text("externalBotId").nullable()
    val botName = text("botName").nullable()
    val externalRecordingId = text("externalRecordingId").nullable()
    val transcriptRequestedAt = pgTimestamp("transcriptRequestedAt", 3).nullable()
    val externalTranscriptId = text("externalTranscriptId").nullable()
    val meetingUrl = text("meetingUrl")
    val normalizedMeetingUrl = text("normalizedMeetingUrl")
    val activeKey = text("activeKey").nullable()
    val meetingStartTime = pgTimestamp("meetingStartTime", 3)
    val status =
        pgEnum("status", "\"MeetingRecordingStatus\"", MeetingRecordingStatus.entries, MeetingRecordingStatus::dbName).default(
            MeetingRecordingStatus.PENDING,
        )
    val failureReason = text("failureReason").nullable()
    val transcript = jsonb<String>("transcript", { it }, { it }).nullable()
    val transcriptFetchedAt = pgTimestamp("transcriptFetchedAt", 3).nullable()
    val mediaDeletedAt = pgTimestamp("mediaDeletedAt", 3).nullable()
    val emailAccountId =
        reference(
            "emailAccountId",
            EmailAccountTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"MeetingRecording_emailAccountId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"MeetingRecording_pkey\"")

    init {
        index("\"MeetingRecording_normalizedMeetingUrl_meetingStartTime_idx\"", false, normalizedMeetingUrl, meetingStartTime)
        index("\"MeetingRecording_status_meetingStartTime_idx\"", false, status, meetingStartTime)
        index("\"MeetingRecording_status_mediaDeletedAt_idx\"", false, status, mediaDeletedAt)
        index("\"MeetingRecording_emailAccountId_idx\"", false, emailAccountId)
        uniqueIndex("\"MeetingRecording_botProvider_externalBotId_key\"", botProvider, externalBotId)
        uniqueIndex("\"MeetingRecording_activeKey_meetingStartTime_key\"", activeKey, meetingStartTime)
    }
}
