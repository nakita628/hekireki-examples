package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class LiteLLMWorkflowRunEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LiteLLMWorkflowRunEntity>(LiteLLMWorkflowRunTable)

    var sessionId by LiteLLMWorkflowRunTable.sessionId
    var workflowType by LiteLLMWorkflowRunTable.workflowType
    var status by LiteLLMWorkflowRunTable.status
    var createdBy by LiteLLMWorkflowRunTable.createdBy
    var createdAt by LiteLLMWorkflowRunTable.createdAt
    var updatedAt by LiteLLMWorkflowRunTable.updatedAt
    var input by LiteLLMWorkflowRunTable.input
    var output by LiteLLMWorkflowRunTable.output
    var metadata by LiteLLMWorkflowRunTable.metadata
    val events by LiteLLMWorkflowEventEntity referrersOn LiteLLMWorkflowEventTable.runId
    val messages by LiteLLMWorkflowMessageEntity referrersOn LiteLLMWorkflowMessageTable.runId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(LiteLLMWorkflowRunTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
