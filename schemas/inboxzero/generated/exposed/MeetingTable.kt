package inboxzero.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object MeetingTable : IdTable<String>("\"Meeting\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val calendarEventId = text("calendarEventId")
    val eventTitle = text("eventTitle")
    val startTime = pgTimestamp("startTime", 3)
    val endTime = pgTimestamp("endTime", 3)
    val attendees = jsonb<String>("attendees", { it }, { it })
    val organizerEmail = text("organizerEmail").nullable()
    val joinOverride = bool("joinOverride").nullable()
    val processingStatus =
        pgEnum("processingStatus", "\"MeetingProcessingStatus\"", MeetingProcessingStatus.entries, MeetingProcessingStatus::dbName).default(
            MeetingProcessingStatus.PENDING,
        )
    val processingAttempts = integer("processingAttempts").default(0)
    val summary = jsonb<String>("summary", { it }, { it }).nullable()
    val followUpDraftStartedAt = pgTimestamp("followUpDraftStartedAt", 3).nullable()
    val followUpDraftId = text("followUpDraftId").nullable()
    val recapSentAt = pgTimestamp("recapSentAt", 3).nullable()
    val recordingId =
        optReference(
            "recordingId",
            MeetingRecordingTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Meeting_recordingId_fkey\"",
        )
    val emailAccountId =
        reference(
            "emailAccountId",
            EmailAccountTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Meeting_emailAccountId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"Meeting_pkey\"")

    init {
        index("\"Meeting_emailAccountId_startTime_idx\"", false, emailAccountId, startTime)
        index("\"Meeting_recordingId_idx\"", false, recordingId)
        index("\"Meeting_processingStatus_updatedAt_idx\"", false, processingStatus, updatedAt)
        uniqueIndex("\"Meeting_emailAccountId_calendarEventId_key\"", emailAccountId, calendarEventId)
        uniqueIndex("\"Meeting_emailAccountId_recordingId_key\"", emailAccountId, recordingId)
    }
}
