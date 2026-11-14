package langwatch.models

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

    var projectId by WorkflowTable.projectId
    var name by WorkflowTable.name
    var icon by WorkflowTable.icon
    var description by WorkflowTable.description
    var createdAt by WorkflowTable.createdAt
    var updatedAt by WorkflowTable.updatedAt
    var latestVersionId by WorkflowTable.latestVersionId
    var currentVersionId by WorkflowTable.currentVersionId
    var publishedId by WorkflowTable.publishedId
    var publishedById by WorkflowTable.publishedById
    var archivedAt by WorkflowTable.archivedAt
    var isEvaluator by WorkflowTable.isEvaluator
    var isComponent by WorkflowTable.isComponent
    var copiedFromWorkflowId by WorkflowTable.copiedFromWorkflowId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(WorkflowTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
