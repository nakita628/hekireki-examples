package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class WorkflowVersionEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, WorkflowVersionEntity>(WorkflowVersionTable)

    var version by WorkflowVersionTable.version
    var commitMessage by WorkflowVersionTable.commitMessage
    var authorId by WorkflowVersionTable.authorId
    var projectId by WorkflowVersionTable.projectId
    var workflowId by WorkflowVersionTable.workflowId
    var parentId by WorkflowVersionTable.parentId
    var autoSaved by WorkflowVersionTable.autoSaved
    var createdAt by WorkflowVersionTable.createdAt
    var updatedAt by WorkflowVersionTable.updatedAt
    var dsl by WorkflowVersionTable.dsl

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(WorkflowVersionTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
