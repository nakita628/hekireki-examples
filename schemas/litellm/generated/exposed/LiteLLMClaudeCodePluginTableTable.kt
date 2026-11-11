package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object LiteLLMClaudeCodePluginTableTable : IdTable<String>("\"LiteLLM_ClaudeCodePluginTable\"") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val name = text("name").uniqueIndex("\"LiteLLM_ClaudeCodePluginTable_name_key\"")
    val version = text("version").nullable()
    val description = text("description").nullable()
    val manifestJson = text("manifest_json").nullable()
    val filesJson = text("files_json").nullable().default("{}")
    val enabled = bool("enabled").default(true)
    val createdAt =
        pgTimestamp("created_at", 3).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt =
        pgTimestamp("updated_at", 3).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val createdBy = text("created_by").nullable()
    val anonymousDownloadCount = long("anonymous_download_count").default(0L)
    override val primaryKey = PrimaryKey(id, name = "\"LiteLLM_ClaudeCodePluginTable_pkey\"")
}
