package litellm.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object LiteLLMClaudeCodePluginDownloadTable : IdTable<Long>("\"LiteLLM_ClaudeCodePluginDownload\"") {
    override val id = long("id").autoIncrement().entityId()
    val pluginId =
        reference(
            "plugin_id",
            LiteLLMClaudeCodePluginTableTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"LiteLLM_ClaudeCodePluginDownload_plugin_id_fkey\"",
        )
    val downloadedAt = pgTimestamp("downloaded_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val version = text("version").nullable()
    val digest = text("digest")
    val authenticated = bool("authenticated")
    val userId = text("user_id").nullable()
    val userEmail = text("user_email").nullable()
    val keyAlias = text("key_alias").nullable()
    val teamId = text("team_id").nullable()
    val teamAlias = text("team_alias").nullable()
    override val primaryKey = PrimaryKey(id, name = "\"LiteLLM_ClaudeCodePluginDownload_pkey\"")

    init {
        index("\"LiteLLM_ClaudeCodePluginDownload_plugin_id_id_idx\"", false, pluginId, id)
    }
}
