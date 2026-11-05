package inboxzero.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object ActionTable : IdTable<String>("\"Action\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val type = pgEnum("type", "\"ActionType\"", ActionType.entries, ActionType::dbName)
    val ruleId = text("ruleId")
    val emailAccountId =
        reference(
            "emailAccountId",
            EmailAccountTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Action_emailAccountId_fkey\"",
        )
    val messagingChannelId = text("messagingChannelId").nullable()
    val messagingChannelEmailAccountId = text("messagingChannelEmailAccountId").nullable()
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
    val delayInMinutes = integer("delayInMinutes").nullable()
    val staticAttachments = jsonb<String>("staticAttachments", { it }, { it }).nullable()
    val integrationName = text("integrationName").nullable()
    val integrationToolName = text("integrationToolName").nullable()
    val integrationArgs = jsonb<String>("integrationArgs", { it }, { it }).nullable()
    override val primaryKey = PrimaryKey(id, name = "\"Action_pkey\"")

    init {
        index("\"Action_ruleId_idx\"", false, ruleId)
        index("\"Action_emailAccountId_idx\"", false, emailAccountId)
        index("\"Action_messagingChannelId_messagingChannelEmailAccountId_idx\"", false, messagingChannelId, messagingChannelEmailAccountId)
        foreignKey(
            ruleId to RuleTable.id,
            emailAccountId to RuleTable.emailAccountId,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            name = "\"Action_ruleId_emailAccountId_fkey\"",
        )
        foreignKey(
            messagingChannelId to MessagingChannelTable.id,
            messagingChannelEmailAccountId to MessagingChannelTable.emailAccountId,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            name = "\"Action_messagingChannelId_messagingChannelEmailAccountId_fkey\"",
        )
    }
}
