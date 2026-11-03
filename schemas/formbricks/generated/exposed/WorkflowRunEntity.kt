package formbricks.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class WorkflowRunEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, WorkflowRunEntity>(WorkflowRunTable)

    var createdAt by WorkflowRunTable.createdAt
    var updatedAt by WorkflowRunTable.updatedAt
    var workflowId by WorkflowRunTable.workflowId
    var workspace by WorkspaceEntity referencedOn WorkflowRunTable.workspaceId
    var workspaceId by WorkflowRunTable.workspaceId
    var workflowVersion by WorkflowVersionEntity optionalReferencedOn WorkflowRunTable.workflowVersionId
    var workflowVersionId by WorkflowRunTable.workflowVersionId
    var response by ResponseEntity optionalReferencedOn WorkflowRunTable.responseId
    var responseId by WorkflowRunTable.responseId
    var status by WorkflowRunTable.status
    var triggerType by WorkflowRunTable.triggerType
    var surveyId by WorkflowRunTable.surveyId
    var isDryRun by WorkflowRunTable.isDryRun
    var idempotencyKey by WorkflowRunTable.idempotencyKey
    var attempt by WorkflowRunTable.attempt
    var nextAttemptAt by WorkflowRunTable.nextAttemptAt
    var lastErrorAt by WorkflowRunTable.lastErrorAt
    var triggerPayload by WorkflowRunTable.triggerPayload
    var data by WorkflowRunTable.data
    var error by WorkflowRunTable.error
    var dispatchedAt by WorkflowRunTable.dispatchedAt
    var startedAt by WorkflowRunTable.startedAt
    var finishedAt by WorkflowRunTable.finishedAt
    val logs by WorkflowRunLogEntity referrersOn WorkflowRunLogTable.runId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(WorkflowRunTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
