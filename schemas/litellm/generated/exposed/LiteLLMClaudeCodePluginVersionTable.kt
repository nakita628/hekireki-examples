package litellm.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.util.UUID

object LiteLLMClaudeCodePluginVersionTable : IdTable<String>("\"LiteLLM_ClaudeCodePluginVersion\"") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val pluginId =
        reference(
            "plugin_id",
            LiteLLMClaudeCodePluginTableTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"LiteLLM_ClaudeCodePluginVersion_plugin_id_fkey\"",
        )
    val version = text("version").nullable()
    val description = text("description").nullable()
    val manifestJson = text("manifest_json").nullable()
    val publishedAt = pgTimestamp("published_at", 3).nullable()
    val publishedBy = text("published_by").nullable()
    val archiveAvailable = bool("archive_available").default(false)
    val content = binary("content").nullable()
    override val primaryKey = PrimaryKey(id, name = "\"LiteLLM_ClaudeCodePluginVersion_pkey\"")

    init {
        index("\"LiteLLM_ClaudeCodePluginVersion_plugin_id_published_at_idx\"", false, pluginId, publishedAt)
        uniqueIndex("\"LiteLLM_ClaudeCodePluginVersion_plugin_id_version_key\"", pluginId, version)
    }
}
