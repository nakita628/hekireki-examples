package rallly.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object ScheduledEventInviteTable : IdTable<String>("scheduled_event_invites") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val uid = text("uid").uniqueIndex("scheduled_event_invites_uid_key")
    val scheduledEventId =
        reference(
            "scheduled_event_id",
            ScheduledEventTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "scheduled_event_invites_scheduled_event_id_fkey",
        )
    val inviteeName = text("invitee_name")
    val inviteeEmail = pgCitext("invitee_email")
    val inviteeId =
        optReference(
            "invitee_id",
            UserTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "scheduled_event_invites_invitee_id_fkey",
        )
    val inviteeTimeZone = text("invitee_time_zone").nullable()
    val inviteeLocale = text("invitee_locale").nullable()
    val status =
        pgEnum("status", "\"scheduled_event_invite_status\"", ScheduledEventInviteStatus.entries, ScheduledEventInviteStatus::dbName)
            .default(ScheduledEventInviteStatus.PENDING)
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "scheduled_event_invites_pkey")

    init {
        index("scheduled_event_invites_invitee_id_idx", false, inviteeId)
        index("scheduled_event_invites_invitee_email_idx", false, inviteeEmail)
        uniqueIndex("scheduled_event_invites_scheduled_event_id_invitee_email_key", scheduledEventId, inviteeEmail)
    }
}
