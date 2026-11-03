package rallly.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object PollActivityTable : IdTable<String>("poll_activities") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val pollId =
        reference(
            "poll_id",
            PollTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "poll_activities_poll_id_fkey",
        )
    val type = text("type")
    val userId = text("user_id").nullable()
    val participantId = text("participant_id").nullable()
    val inviteId = text("invite_id").nullable()
    val optionId = text("option_id").nullable()
    val payload = jsonb<String>("payload", { it }, { it }).default("{}")
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "poll_activities_pkey")

    init {
        index(
            "poll_activities_poll_id_created_at_idx",
            false,
            functions = listOf(indexExpression("\"poll_id\""), indexExpression("\"created_at\" DESC")),
        )
        index("poll_activities_poll_id_participant_id_idx", false, pollId, participantId)
        index("poll_activities_poll_id_option_id_idx", false, pollId, optionId)
        index("poll_activities_poll_id_invite_id_idx", false, pollId, inviteId)
        index("poll_activities_user_id_idx", false, userId)
    }
}
