package inboxzero.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object ScheduledActionTable : IdTable<String>("\"ScheduledAction\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val executedRuleId =
        reference(
            "executedRuleId",
            ExecutedRuleTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"ScheduledAction_executedRuleId_fkey\"",
        )
    val actionType = pgEnum("actionType", "\"ActionType\"", ActionType.entries, ActionType::dbName)
    val messageId = text("messageId")
    val threadId = text("threadId")
    val scheduledFor = pgTimestamp("scheduledFor", 3)
    val emailAccountId =
        reference(
            "emailAccountId",
            EmailAccountTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"ScheduledAction_emailAccountId_fkey\"",
        )
    val status =
        pgEnum("status", "\"ScheduledActionStatus\"", ScheduledActionStatus.entries, ScheduledActionStatus::dbName).default(
            ScheduledActionStatus.PENDING,
        )
    val schedulingStatus =
        pgEnum("schedulingStatus", "\"SchedulingStatus\"", SchedulingStatus.entries, SchedulingStatus::dbName).default(
            SchedulingStatus.PENDING,
        )
    val label = text("label").nullable()
    val labelId = text("labelId").nullable()
    val subject = text("subject").nullable()
    val content = text("content").nullable()
    val to = text("to").nullable()
    val cc = text("cc").nullable()
    val bcc = text("bcc").nullable()
    val url = text("url").nullable()
    val folderName = text("folderName").nullable()
    val folderId = text("folderId").nullable()
    val scheduledId = text("scheduledId").nullable()
    val staticAttachments = jsonb<String>("staticAttachments", { it }, { it }).nullable()
    val selectedAttachments = jsonb<String>("selectedAttachments", { it }, { it }).nullable()
    val executedAt = pgTimestamp("executedAt", 3).nullable()
    val executedActionId =
        optReference(
            "executedActionId",
            ExecutedActionTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"ScheduledAction_executedActionId_fkey\"",
        ).uniqueIndex("\"ScheduledAction_executedActionId_key\"")
    override val primaryKey = PrimaryKey(id, name = "\"ScheduledAction_pkey\"")

    init {
        index("\"ScheduledAction_executedRuleId_idx\"", false, executedRuleId)
        index("\"ScheduledAction_status_scheduledFor_idx\"", false, status, scheduledFor)
        index("\"ScheduledAction_emailAccountId_messageId_idx\"", false, emailAccountId, messageId)
    }
}
