package rallly.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object PollTable : IdTable<String>("polls") {
    override val id = text("id").entityId()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val deadline = pgTimestamp("deadline", 3).nullable()
    val title = text("title")
    val description = text("description").nullable()
    val location = text("location").nullable()
    val conferencing = jsonb<String>("conferencing", { it }, { it }).nullable()
    val userId =
        optReference(
            "user_id",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "polls_user_id_fkey",
        )
    val timeZone = text("time_zone").nullable()
    val status = pgEnum("status", "\"poll_status\"", PollStatus.entries, PollStatus::dbName).default(PollStatus.OPEN)
    val closedReason = pgEnum("closed_reason", "\"poll_closed_reason\"", PollClosedReason.entries, PollClosedReason::dbName).nullable()
    val kind = pgEnum("kind", "\"poll_kind\"", PollKind.entries, PollKind::dbName).default(PollKind.DATE)
    val deleted = bool("deleted").default(false)
    val deletedAt = pgTimestamp("deleted_at", 3).nullable()
    val eventId = text("event_id").nullable().uniqueIndex("polls_event_id_key")
    val scheduledEventId =
        optReference(
            "scheduled_event_id",
            ScheduledEventTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "polls_scheduled_event_id_fkey",
        )
    val hideParticipants = bool("hide_participants").default(false)
    val hideScores = bool("hide_scores").default(false)
    val disableComments = bool("disable_comments").default(true)
    val requireParticipantEmail = bool("require_participant_email").default(false)
    val allowTentativeVotes = bool("allow_tentative_votes").default(true)
    val muted = bool("muted").default(false)
    val spaceId =
        optReference(
            "space_id",
            SpaceTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "polls_space_id_fkey",
        )
    override val primaryKey = PrimaryKey(id, name = "polls_pkey")

    init {
        uniqueIndex("polls_id_key", id)
        index("polls_space_id_idx", false, spaceId)
        index("polls_user_id_idx", false, userId)
    }
}
