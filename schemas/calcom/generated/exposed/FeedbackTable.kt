package calcom.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object FeedbackTable : IdTable<Int>("\"Feedback\"") {
    override val id = integer("id").autoIncrement().entityId()
    val date = pgTimestamp("date", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val userId =
        reference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Feedback_userId_fkey\"",
        )
    val rating = text("rating")
    val comment = text("comment").nullable()
    override val primaryKey = PrimaryKey(id, name = "\"Feedback_pkey\"")

    init {
        index("\"Feedback_userId_idx\"", false, userId)
        index("\"Feedback_rating_idx\"", false, rating)
    }
}
