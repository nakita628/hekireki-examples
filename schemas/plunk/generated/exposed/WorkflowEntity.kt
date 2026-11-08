package plunk.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class WorkflowEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, WorkflowEntity>(WorkflowTable)

    var name by WorkflowTable.name
    var description by WorkflowTable.description
    var enabled by WorkflowTable.enabled
    var triggerType by WorkflowTable.triggerType
    var triggerConfig by WorkflowTable.triggerConfig
    var allowReentry by WorkflowTable.allowReentry
    var throttleWindowSeconds by WorkflowTable.throttleWindowSeconds
    var throttleKey by WorkflowTable.throttleKey
    var project by ProjectEntity referencedOn WorkflowTable.projectId
    var projectId by WorkflowTable.projectId
    val steps by WorkflowStepEntity referrersOn WorkflowStepTable.workflowId
    val executions by WorkflowExecutionEntity referrersOn WorkflowExecutionTable.workflowId
    val throttleClaims by ThrottleClaimEntity optionalReferrersOn ThrottleClaimTable.workflowId
    var createdAt by WorkflowTable.createdAt
    var updatedAt by WorkflowTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(WorkflowTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
