package rallly.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object OptionTable : IdTable<String>("options") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val startTime = pgTimestamp("start_time", 0)
    val duration = integer("duration_minutes").default(0)
    val pollId =
        reference(
            "poll_id",
            PollTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "options_poll_id_fkey",
        )
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "options_pkey")

    init {
        index("options_poll_id_idx", false, pollId, indexType = "hash")
    }
}
