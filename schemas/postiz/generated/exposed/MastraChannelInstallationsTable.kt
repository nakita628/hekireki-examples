package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object MastraChannelInstallationsTable : IdTable<String>("mastra_channel_installations") {
    override val id = text("id").entityId()
    val platform = text("platform")
    val agentId = text("agentId")
    val status = text("status")
    val webhookId = text("webhookId").nullable().uniqueIndex("idx_channel_installations_webhook")
    val data = jsonb<String>("data", { it }, { it })
    val configHash = text("configHash").nullable()
    val error = text("error").nullable()
    val createdAt = pgTimestamp("createdAt", 6)
    val updatedAt = pgTimestamp("updatedAt", 6)
    val createdAtZ =
        pgTimestamptz("createdAtZ", 6).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAtZ =
        pgTimestamptz("updatedAtZ", 6).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "mastra_channel_installations_pkey")

    init {
        index("idx_channel_installations_platform_agent", false, platform, agentId)
    }
}
