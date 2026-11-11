package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class LiteLLMClaudeCodePluginDownloadEntity(
    id: EntityID<Long>,
) : Entity<Long>(id) {
    companion object : EntityClass<Long, LiteLLMClaudeCodePluginDownloadEntity>(LiteLLMClaudeCodePluginDownloadTable)

    var pluginId by LiteLLMClaudeCodePluginDownloadTable.pluginId
    var downloadedAt by LiteLLMClaudeCodePluginDownloadTable.downloadedAt
    var version by LiteLLMClaudeCodePluginDownloadTable.version
    var digest by LiteLLMClaudeCodePluginDownloadTable.digest
    var authenticated by LiteLLMClaudeCodePluginDownloadTable.authenticated
    var userId by LiteLLMClaudeCodePluginDownloadTable.userId
    var userEmail by LiteLLMClaudeCodePluginDownloadTable.userEmail
    var keyAlias by LiteLLMClaudeCodePluginDownloadTable.keyAlias
    var teamId by LiteLLMClaudeCodePluginDownloadTable.teamId
    var teamAlias by LiteLLMClaudeCodePluginDownloadTable.teamAlias
    var plugin by LiteLLMClaudeCodePluginTableEntity referencedOn LiteLLMClaudeCodePluginDownloadTable.pluginId
}
