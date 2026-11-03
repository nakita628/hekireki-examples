package formbricks.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class WorkflowVersionEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, WorkflowVersionEntity>(WorkflowVersionTable)

    var workflowId by WorkflowVersionTable.workflowId
    var workspaceId by WorkflowVersionTable.workspaceId
    var version by WorkflowVersionTable.version
    var definition by WorkflowVersionTable.definition
    var publishedAt by WorkflowVersionTable.publishedAt
    var publisher by UserEntity optionalReferencedOn WorkflowVersionTable.publishedBy
    var publishedBy by WorkflowVersionTable.publishedBy
    val runs by WorkflowRunEntity optionalReferrersOn WorkflowRunTable.workflowVersionId
}
