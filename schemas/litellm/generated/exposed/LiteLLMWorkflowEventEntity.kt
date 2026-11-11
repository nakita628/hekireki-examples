package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class LiteLLMWorkflowEventEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LiteLLMWorkflowEventEntity>(LiteLLMWorkflowEventTable)

    var runId by LiteLLMWorkflowEventTable.runId
    var eventType by LiteLLMWorkflowEventTable.eventType
    var stepName by LiteLLMWorkflowEventTable.stepName
    var sequenceNumber by LiteLLMWorkflowEventTable.sequenceNumber
    var data by LiteLLMWorkflowEventTable.data
    var createdAt by LiteLLMWorkflowEventTable.createdAt
    var run by LiteLLMWorkflowRunEntity referencedOn LiteLLMWorkflowEventTable.runId
}
