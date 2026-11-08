package plunk.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class WorkflowTransitionEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, WorkflowTransitionEntity>(WorkflowTransitionTable)

    var fromStep by WorkflowStepEntity referencedOn WorkflowTransitionTable.fromStepId
    var fromStepId by WorkflowTransitionTable.fromStepId
    var toStep by WorkflowStepEntity referencedOn WorkflowTransitionTable.toStepId
    var toStepId by WorkflowTransitionTable.toStepId
    var condition by WorkflowTransitionTable.condition
    var priority by WorkflowTransitionTable.priority
    var createdAt by WorkflowTransitionTable.createdAt
    var updatedAt by WorkflowTransitionTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(WorkflowTransitionTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
