package rallly.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object ParticipantTable : IdTable<String>("participants") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val name = text("name")
    val email = text("email").nullable()
    val note = text("note").nullable()
    val token = text("token").uniqueIndex("participants_token_key")
    val userId =
        optReference(
            "user_id",
            UserTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "participants_user_id_fkey",
        )
    val pollId =
        reference(
            "poll_id",
            PollTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "participants_poll_id_fkey",
        )
    val locale = text("locale").nullable()
    val timeZone = text("time_zone").nullable()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).nullable().clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "participants_pkey")

    init {
        index("participants_poll_id_idx", false, pollId, indexType = "hash")
        index("participants_user_id_idx", false, userId)
    }
}
