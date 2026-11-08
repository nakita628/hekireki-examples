package plunk.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class WorkflowStepExecutionEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, WorkflowStepExecutionEntity>(WorkflowStepExecutionTable)

    var execution by WorkflowExecutionEntity referencedOn WorkflowStepExecutionTable.executionId
    var executionId by WorkflowStepExecutionTable.executionId
    var step by WorkflowStepEntity referencedOn WorkflowStepExecutionTable.stepId
    var stepId by WorkflowStepExecutionTable.stepId
    var status by WorkflowStepExecutionTable.status
    var scheduledFor by WorkflowStepExecutionTable.scheduledFor
    var executeAfter by WorkflowStepExecutionTable.executeAfter
    var output by WorkflowStepExecutionTable.output
    var error by WorkflowStepExecutionTable.error
    val emails by EmailEntity optionalReferrersOn EmailTable.workflowStepExecutionId
    var startedAt by WorkflowStepExecutionTable.startedAt
    var completedAt by WorkflowStepExecutionTable.completedAt
    var createdAt by WorkflowStepExecutionTable.createdAt
    var updatedAt by WorkflowStepExecutionTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(WorkflowStepExecutionTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
