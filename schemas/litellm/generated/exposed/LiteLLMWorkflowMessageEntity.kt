package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class LiteLLMWorkflowMessageEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LiteLLMWorkflowMessageEntity>(LiteLLMWorkflowMessageTable)

    var runId by LiteLLMWorkflowMessageTable.runId
    var role by LiteLLMWorkflowMessageTable.role
    var content by LiteLLMWorkflowMessageTable.content
    var sequenceNumber by LiteLLMWorkflowMessageTable.sequenceNumber
    var sessionId by LiteLLMWorkflowMessageTable.sessionId
    var createdAt by LiteLLMWorkflowMessageTable.createdAt
    var run by LiteLLMWorkflowRunEntity referencedOn LiteLLMWorkflowMessageTable.runId
}
