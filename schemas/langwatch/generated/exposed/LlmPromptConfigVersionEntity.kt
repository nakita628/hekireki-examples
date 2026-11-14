package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class LlmPromptConfigVersionEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LlmPromptConfigVersionEntity>(LlmPromptConfigVersionTable)

    var version by LlmPromptConfigVersionTable.version
    var commitMessage by LlmPromptConfigVersionTable.commitMessage
    var authorId by LlmPromptConfigVersionTable.authorId
    var configId by LlmPromptConfigVersionTable.configId
    var configData by LlmPromptConfigVersionTable.configData
    var schemaVersion by LlmPromptConfigVersionTable.schemaVersion
    var runtimeParameters by LlmPromptConfigVersionTable.runtimeParameters
    var createdAt by LlmPromptConfigVersionTable.createdAt
    var projectId by LlmPromptConfigVersionTable.projectId
}
