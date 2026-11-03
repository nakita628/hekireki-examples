package papermark.models

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
    var entryLinkId by WorkflowTable.entryLinkId
    var teamId by WorkflowTable.teamId
    var isActive by WorkflowTable.isActive
    var createdAt by WorkflowTable.createdAt
    var updatedAt by WorkflowTable.updatedAt
    var entryLink by LinkEntity referencedOn WorkflowTable.entryLinkId
    var team by TeamEntity referencedOn WorkflowTable.teamId
    val steps by WorkflowStepEntity referrersOn WorkflowStepTable.workflowId
    val executions by WorkflowExecutionEntity referrersOn WorkflowExecutionTable.workflowId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(WorkflowTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
