package papermark.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class WorkflowStepLogEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, WorkflowStepLogEntity>(WorkflowStepLogTable)

    var executionId by WorkflowStepLogTable.executionId
    var workflowStepId by WorkflowStepLogTable.workflowStepId
    var conditionsMatched by WorkflowStepLogTable.conditionsMatched
    var conditionResults by WorkflowStepLogTable.conditionResults
    var actionsExecuted by WorkflowStepLogTable.actionsExecuted
    var executedAt by WorkflowStepLogTable.executedAt
    var duration by WorkflowStepLogTable.duration
    var error by WorkflowStepLogTable.error
    var execution by WorkflowExecutionEntity referencedOn WorkflowStepLogTable.executionId
    var step by WorkflowStepEntity referencedOn WorkflowStepLogTable.workflowStepId
}
