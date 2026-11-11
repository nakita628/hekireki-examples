package litellm.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable

object LiteLLMClaudeCodePluginUploadTable : IdTable<String>("\"LiteLLM_ClaudeCodePluginUpload\"") {
    override val id =
        text("plugin_name")
            .references(
                LiteLLMClaudeCodePluginTableTable.name,
                onDelete = ReferenceOption.CASCADE,
                onUpdate = ReferenceOption.CASCADE,
                fkName = "\"LiteLLM_ClaudeCodePluginUpload_plugin_name_fkey\"",
            ).entityId()
    val content = binary("content")
    override val primaryKey = PrimaryKey(id, name = "\"LiteLLM_ClaudeCodePluginUpload_pkey\"")
}
