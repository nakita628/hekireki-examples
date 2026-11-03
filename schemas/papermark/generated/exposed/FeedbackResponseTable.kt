package papermark.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object FeedbackResponseTable : IdTable<String>("\"FeedbackResponse\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val feedbackId =
        reference(
            "feedbackId",
            FeedbackTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"FeedbackResponse_feedbackId_fkey\"",
        )
    val data = jsonb<String>("data", { it }, { it })
    val viewId =
        reference(
            "viewId",
            ViewTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"FeedbackResponse_viewId_fkey\"",
        ).uniqueIndex("\"FeedbackResponse_viewId_key\"")
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"FeedbackResponse_pkey\"")

    init {
        index("\"FeedbackResponse_feedbackId_idx\"", false, feedbackId)
        index("\"FeedbackResponse_viewId_idx\"", false, viewId)
    }
}
