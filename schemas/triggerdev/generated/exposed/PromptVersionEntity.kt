package triggerdev.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class PromptVersionEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, PromptVersionEntity>(PromptVersionTable)

    var version by PromptVersionTable.version
    var prompt by PromptEntity referencedOn PromptVersionTable.promptId
    var promptId by PromptVersionTable.promptId
    var textContent by PromptVersionTable.textContent
    var chatContent by PromptVersionTable.chatContent
    var model by PromptVersionTable.model
    var config by PromptVersionTable.config
    var source by PromptVersionTable.source1
    var commitMessage by PromptVersionTable.commitMessage
    var contentHash by PromptVersionTable.contentHash
    var labels by PromptVersionTable.labels
    var worker by BackgroundWorkerEntity optionalReferencedOn PromptVersionTable.workerId
    var workerId by PromptVersionTable.workerId
    var createdBy by PromptVersionTable.createdBy
    var createdAt by PromptVersionTable.createdAt
}
