package papermark.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object YearInReviewTable : IdTable<String>("\"YearInReview\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val teamId = text("teamId")
    val status = text("status").default("pending")
    val attempts = integer("attempts").default(0)
    val lastAttempted = pgTimestamp("lastAttempted", 3).nullable()
    val error = text("error").nullable()
    val stats = jsonb<String>("stats", { it }, { it })
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"YearInReview_pkey\"")

    init {
        index("\"YearInReview_status_attempts_idx\"", false, status, attempts)
        index("\"YearInReview_teamId_idx\"", false, teamId)
    }
}
