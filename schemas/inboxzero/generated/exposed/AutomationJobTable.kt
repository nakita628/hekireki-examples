package inboxzero.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object AutomationJobTable : IdTable<String>("\"AutomationJob\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val name = text("name")
    val enabled = bool("enabled").default(true)
    val prompt = text("prompt").nullable()
    val cronExpression = text("cronExpression")
    val nextRunAt = pgTimestamp("nextRunAt", 3)
    val messagingChannelId = text("messagingChannelId")
    val emailAccountId = text("emailAccountId").uniqueIndex("\"AutomationJob_emailAccountId_key\"")
    override val primaryKey = PrimaryKey(id, name = "\"AutomationJob_pkey\"")

    init {
        index("\"AutomationJob_emailAccountId_enabled_nextRunAt_idx\"", false, emailAccountId, enabled, nextRunAt)
        index("\"AutomationJob_enabled_nextRunAt_idx\"", false, enabled, nextRunAt)
        index("\"AutomationJob_messagingChannelId_emailAccountId_idx\"", false, messagingChannelId, emailAccountId)
        foreignKey(
            messagingChannelId to MessagingChannelTable.id,
            emailAccountId to MessagingChannelTable.emailAccountId,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            name = "\"AutomationJob_messagingChannelId_emailAccountId_fkey\"",
        )
    }
}
