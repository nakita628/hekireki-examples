package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object EmailSuppressionTable : IdTable<String>("\"EmailSuppression\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val projectId = text("projectId")
    val email = text("email")
    val triggerId = text("triggerId").nullable()
    val reason = text("reason").default("unsubscribe")
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"EmailSuppression_pkey\"")

    init {
        index("\"EmailSuppression_projectId_idx\"", false, projectId)
        uniqueIndex("\"EmailSuppression_projectId_email_triggerId_key\"", projectId, email, triggerId)
    }
}
