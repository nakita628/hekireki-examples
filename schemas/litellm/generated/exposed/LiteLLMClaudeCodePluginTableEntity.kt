package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class LiteLLMClaudeCodePluginTableEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LiteLLMClaudeCodePluginTableEntity>(LiteLLMClaudeCodePluginTableTable)

    var name by LiteLLMClaudeCodePluginTableTable.name
    var version by LiteLLMClaudeCodePluginTableTable.version
    var description by LiteLLMClaudeCodePluginTableTable.description
    var manifestJson by LiteLLMClaudeCodePluginTableTable.manifestJson
    var filesJson by LiteLLMClaudeCodePluginTableTable.filesJson
    var enabled by LiteLLMClaudeCodePluginTableTable.enabled
    var createdAt by LiteLLMClaudeCodePluginTableTable.createdAt
    var updatedAt by LiteLLMClaudeCodePluginTableTable.updatedAt
    var createdBy by LiteLLMClaudeCodePluginTableTable.createdBy
    val versions by LiteLLMClaudeCodePluginVersionEntity referrersOn LiteLLMClaudeCodePluginVersionTable.pluginId
    val downloads by LiteLLMClaudeCodePluginDownloadEntity referrersOn LiteLLMClaudeCodePluginDownloadTable.pluginId
    var anonymousDownloadCount by LiteLLMClaudeCodePluginTableTable.anonymousDownloadCount

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(LiteLLMClaudeCodePluginTableTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
