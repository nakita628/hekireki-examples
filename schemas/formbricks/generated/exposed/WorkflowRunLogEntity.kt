package formbricks.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class WorkflowRunLogEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, WorkflowRunLogEntity>(WorkflowRunLogTable)

    var run by WorkflowRunEntity referencedOn WorkflowRunLogTable.runId
    var runId by WorkflowRunLogTable.runId
    var sequence by WorkflowRunLogTable.sequence
    var stepId by WorkflowRunLogTable.stepId
    var stepType by WorkflowRunLogTable.stepType
    var status by WorkflowRunLogTable.status
    var input by WorkflowRunLogTable.input
    var output by WorkflowRunLogTable.output
    var error by WorkflowRunLogTable.error
    var startedAt by WorkflowRunLogTable.startedAt
    var finishedAt by WorkflowRunLogTable.finishedAt
}
