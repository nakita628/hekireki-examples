package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class LiteLLMClaudeCodePluginVersionEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LiteLLMClaudeCodePluginVersionEntity>(LiteLLMClaudeCodePluginVersionTable)

    var pluginId by LiteLLMClaudeCodePluginVersionTable.pluginId
    var version by LiteLLMClaudeCodePluginVersionTable.version
    var description by LiteLLMClaudeCodePluginVersionTable.description
    var manifestJson by LiteLLMClaudeCodePluginVersionTable.manifestJson
    var publishedAt by LiteLLMClaudeCodePluginVersionTable.publishedAt
    var publishedBy by LiteLLMClaudeCodePluginVersionTable.publishedBy
    var archiveAvailable by LiteLLMClaudeCodePluginVersionTable.archiveAvailable
    var content by LiteLLMClaudeCodePluginVersionTable.content
    var plugin by LiteLLMClaudeCodePluginTableEntity referencedOn LiteLLMClaudeCodePluginVersionTable.pluginId
}
