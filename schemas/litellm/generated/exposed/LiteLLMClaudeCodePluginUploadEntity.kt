package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class LiteLLMClaudeCodePluginUploadEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LiteLLMClaudeCodePluginUploadEntity>(LiteLLMClaudeCodePluginUploadTable)

    var content by LiteLLMClaudeCodePluginUploadTable.content
}
