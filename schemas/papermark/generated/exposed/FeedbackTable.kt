package papermark.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object FeedbackTable : IdTable<String>("\"Feedback\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val linkId =
        reference(
            "linkId",
            LinkTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Feedback_linkId_fkey\"",
        ).uniqueIndex("\"Feedback_linkId_key\"")
    val data = jsonb<String>("data", { it }, { it })
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"Feedback_pkey\"")

    init {
        index("\"Feedback_linkId_idx\"", false, linkId)
    }
}
