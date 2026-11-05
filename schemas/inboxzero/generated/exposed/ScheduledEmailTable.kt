package inboxzero.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object ScheduledEmailTable : IdTable<String>("\"ScheduledEmail\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val emailAccountId =
        reference(
            "emailAccountId",
            EmailAccountTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"ScheduledEmail_emailAccountId_fkey\"",
        )
    val clientMutationId = text("clientMutationId")
    val payloadHash = text("payloadHash")
    val payload = jsonb<String>("payload", { it }, { it })
    val threadId = text("threadId").nullable()
    val sendAt = pgTimestamp("sendAt", 3)
    val status =
        pgEnum("status", "\"ScheduledEmailStatus\"", ScheduledEmailStatus.entries, ScheduledEmailStatus::dbName).default(
            ScheduledEmailStatus.PENDING,
        )
    val processingStartedAt = pgTimestamp("processingStartedAt", 3).nullable()
    val executionQueuedAt = pgTimestamp("executionQueuedAt", 3).nullable()
    val sentAt = pgTimestamp("sentAt", 3).nullable()
    val error = text("error").nullable()
    val remindAt = pgTimestamp("remindAt", 3).nullable()
    val reminderStatus =
        pgEnum("reminderStatus", "\"EmailReminderStatus\"", EmailReminderStatus.entries, EmailReminderStatus::dbName).default(
            EmailReminderStatus.NONE,
        )
    val reminderStartedAt = pgTimestamp("reminderStartedAt", 3).nullable()
    val heldForUndo = bool("heldForUndo").default(false)
    val draftMessageIds = array("draftMessageIds", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    override val primaryKey = PrimaryKey(id, name = "\"ScheduledEmail_pkey\"")

    init {
        index("\"ScheduledEmail_status_sendAt_idx\"", false, status, sendAt)
        index("\"ScheduledEmail_status_processingStartedAt_idx\"", false, status, processingStartedAt)
        index("\"ScheduledEmail_reminderStatus_remindAt_idx\"", false, reminderStatus, remindAt)
        index("\"ScheduledEmail_reminderStatus_reminderStartedAt_idx\"", false, reminderStatus, reminderStartedAt)
        index("\"ScheduledEmail_emailAccountId_threadId_idx\"", false, emailAccountId, threadId)
        uniqueIndex("\"ScheduledEmail_emailAccountId_clientMutationId_key\"", emailAccountId, clientMutationId)
    }
}
