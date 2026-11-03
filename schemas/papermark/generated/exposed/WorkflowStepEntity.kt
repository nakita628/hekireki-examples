package papermark.models

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

    var workflowId by WorkflowStepTable.workflowId
    var name by WorkflowStepTable.name
    var stepOrder by WorkflowStepTable.stepOrder
    var stepType by WorkflowStepTable.stepType
    var conditions by WorkflowStepTable.conditions
    var actions by WorkflowStepTable.actions
    var createdAt by WorkflowStepTable.createdAt
    var updatedAt by WorkflowStepTable.updatedAt
    var workflow by WorkflowEntity referencedOn WorkflowStepTable.workflowId
    val executionLogs by WorkflowStepLogEntity referrersOn WorkflowStepLogTable.workflowStepId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(WorkflowStepTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
