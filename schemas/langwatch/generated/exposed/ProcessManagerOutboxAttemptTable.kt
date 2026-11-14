package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable

object ProcessManagerOutboxAttemptTable : IdTable<String>("\"ProcessManagerOutboxAttempt\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val outboxId = text("outboxId")
    val projectId = text("projectId")
    val attempt = integer("attempt")
    val occurredAt = pgTimestamptz("occurredAt", 3)
    val outcome =
        pgEnum(
            "outcome",
            "\"ProcessManagerOutboxAttemptOutcome\"",
            ProcessManagerOutboxAttemptOutcome.entries,
            ProcessManagerOutboxAttemptOutcome::dbName,
        )
    val errorType = text("errorType")
    val errorMessage = text("errorMessage")
    val retryAfterMs = integer("retryAfterMs").nullable()
    override val primaryKey = PrimaryKey(id, name = "\"ProcessManagerOutboxAttempt_pkey\"")

    init {
        index("\"ProcessManagerOutboxAttempt_outboxId_attempt_idx\"", false, outboxId, attempt)
    }
}
