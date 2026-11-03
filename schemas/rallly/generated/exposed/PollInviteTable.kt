package rallly.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object PollInviteTable : IdTable<String>("poll_invites") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val pollId =
        reference(
            "poll_id",
            PollTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "poll_invites_poll_id_fkey",
        )
    val email = pgCitext("email")
    val token = text("token").uniqueIndex("poll_invites_token_key")
    val openedAt = pgTimestamp("opened_at", 3).nullable()
    val remindedAt = pgTimestamp("reminded_at", 3).nullable()
    val revokedAt = pgTimestamp("revoked_at", 3).nullable()
    val participantId =
        optReference(
            "participant_id",
            ParticipantTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "poll_invites_participant_id_fkey",
        ).uniqueIndex("poll_invites_participant_id_key")
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "poll_invites_pkey")

    init {
        uniqueIndex("poll_invites_poll_id_email_key", pollId, email)
    }
}
