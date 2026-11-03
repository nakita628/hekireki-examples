package papermark.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class WorkflowExecutionEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, WorkflowExecutionEntity>(WorkflowExecutionTable)

    var workflowId by WorkflowExecutionTable.workflowId
    var visitorEmail by WorkflowExecutionTable.visitorEmail
    var visitorIp by WorkflowExecutionTable.visitorIp
    var status by WorkflowExecutionTable.status
    var startedAt by WorkflowExecutionTable.startedAt
    var completedAt by WorkflowExecutionTable.completedAt
    var result by WorkflowExecutionTable.result
    var metadata by WorkflowExecutionTable.metadata
    var workflow by WorkflowEntity referencedOn WorkflowExecutionTable.workflowId
    val stepLogs by WorkflowStepLogEntity referrersOn WorkflowStepLogTable.executionId
}
