package plunk.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class WorkflowStepEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, WorkflowStepEntity>(WorkflowStepTable)

    var type by WorkflowStepTable.type
    var name by WorkflowStepTable.name
    var position by WorkflowStepTable.position
    var config by WorkflowStepTable.config
    var workflow by WorkflowEntity referencedOn WorkflowStepTable.workflowId
    var workflowId by WorkflowStepTable.workflowId
    var template by TemplateEntity optionalReferencedOn WorkflowStepTable.templateId
    var templateId by WorkflowStepTable.templateId
    val outgoingTransitions by WorkflowTransitionEntity referrersOn WorkflowTransitionTable.fromStepId
    val incomingTransitions by WorkflowTransitionEntity referrersOn WorkflowTransitionTable.toStepId
    val executions by WorkflowStepExecutionEntity referrersOn WorkflowStepExecutionTable.stepId
    val currentWorkflowExecutions by WorkflowExecutionEntity optionalReferrersOn WorkflowExecutionTable.currentStepId
    var createdAt by WorkflowStepTable.createdAt
    var updatedAt by WorkflowStepTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(WorkflowStepTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
