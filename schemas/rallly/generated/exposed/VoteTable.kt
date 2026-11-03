package rallly.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object VoteTable : IdTable<String>("votes") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val participantId =
        reference(
            "participant_id",
            ParticipantTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "votes_participant_id_fkey",
        )
    val optionId =
        reference(
            "option_id",
            OptionTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "votes_option_id_fkey",
        )
    val pollId =
        reference(
            "poll_id",
            PollTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "votes_poll_id_fkey",
        )
    val type = pgEnum("type", "\"vote_type\"", VoteType.entries, VoteType::dbName).default(VoteType.YES)
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).nullable().clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "votes_pkey")

    init {
        index("votes_poll_id_idx", false, pollId, indexType = "hash")
        index("votes_participant_id_idx", false, participantId, indexType = "hash")
        index("votes_option_id_idx", false, optionId, indexType = "hash")
    }
}
