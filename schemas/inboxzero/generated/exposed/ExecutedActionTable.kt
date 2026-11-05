package inboxzero.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object ExecutedActionTable : IdTable<String>("\"ExecutedAction\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val type = pgEnum("type", "\"ActionType\"", ActionType.entries, ActionType::dbName)
    val executionStatus =
        pgEnum("executionStatus", "\"ExecutedActionStatus\"", ExecutedActionStatus.entries, ExecutedActionStatus::dbName).nullable()
    val executionStartedAt = pgTimestamp("executionStartedAt", 3).nullable()
    val executedAt = pgTimestamp("executedAt", 3).nullable()
    val executionError = jsonb<String>("executionError", { it }, { it }).nullable()
    val sentMessageIds = array("sentMessageIds", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val executedRuleId =
        reference(
            "executedRuleId",
            ExecutedRuleTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"ExecutedAction_executedRuleId_fkey\"",
        )
    val messagingChannelId =
        optReference(
            "messagingChannelId",
            MessagingChannelTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"ExecutedAction_messagingChannelId_fkey\"",
        )
    val messagingMessageId = text("messagingMessageId").nullable()
    val messagingMessageSentAt = pgTimestamp("messagingMessageSentAt", 3).nullable()
    val messagingMessageStatus =
        pgEnum("messagingMessageStatus", "\"MessagingMessageStatus\"", MessagingMessageStatus.entries, MessagingMessageStatus::dbName)
            .nullable()
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
    val staticAttachments = jsonb<String>("staticAttachments", { it }, { it }).nullable()
    val selectedAttachments = jsonb<String>("selectedAttachments", { it }, { it }).nullable()
    val integrationName = text("integrationName").nullable()
    val integrationToolName = text("integrationToolName").nullable()
    val integrationArgs = jsonb<String>("integrationArgs", { it }, { it }).nullable()
    val draftId = text("draftId").nullable()
    val draftStatus = pgEnum("draftStatus", "\"DraftEmailStatus\"", DraftEmailStatus.entries, DraftEmailStatus::dbName).nullable()
    val wasDraftSent = bool("wasDraftSent").nullable()
    val draftModelProvider = text("draftModelProvider").nullable()
    val draftModelName = text("draftModelName").nullable()
    val draftPipelineVersion = integer("draftPipelineVersion").nullable()
    val draftContextMetadata = jsonb<String>("draftContextMetadata", { it }, { it }).nullable()
    override val primaryKey = PrimaryKey(id, name = "\"ExecutedAction_pkey\"")

    init {
        index("\"ExecutedAction_executedRuleId_idx\"", false, executedRuleId)
        index("\"ExecutedAction_executionStatus_createdAt_idx\"", false, executionStatus, createdAt)
        index("\"ExecutedAction_sentMessageIds_idx\"", false, sentMessageIds, indexType = "gin")
        index("\"ExecutedAction_messagingChannelId_createdAt_idx\"", false, messagingChannelId, createdAt)
        index(
            "\"ExecutedAction_draftStatus_createdAt_draft_idx\"",
            false,
            draftStatus,
            createdAt,
            filterCondition = sqlCondition("(\"draftId\" IS NOT NULL)"),
        )
        index(
            "\"ExecutedAction_executedRuleId_draft_idx\"",
            false,
            executedRuleId,
            filterCondition = sqlCondition("(\"draftId\" IS NOT NULL)"),
        )
    }
}
