package plunk.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class WorkflowExecutionEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, WorkflowExecutionEntity>(WorkflowExecutionTable)

    var workflow by WorkflowEntity referencedOn WorkflowExecutionTable.workflowId
    var workflowId by WorkflowExecutionTable.workflowId
    var contact by ContactEntity referencedOn WorkflowExecutionTable.contactId
    var contactId by WorkflowExecutionTable.contactId
    var status by WorkflowExecutionTable.status
    var currentStep by WorkflowStepEntity optionalReferencedOn WorkflowExecutionTable.currentStepId
    var currentStepId by WorkflowExecutionTable.currentStepId
    var exitReason by WorkflowExecutionTable.exitReason
    var context by WorkflowExecutionTable.context
    val stepExecutions by WorkflowStepExecutionEntity referrersOn WorkflowStepExecutionTable.executionId
    val emails by EmailEntity optionalReferrersOn EmailTable.workflowExecutionId
    var startedAt by WorkflowExecutionTable.startedAt
    var completedAt by WorkflowExecutionTable.completedAt
    var createdAt by WorkflowExecutionTable.createdAt
    var updatedAt by WorkflowExecutionTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(WorkflowExecutionTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
