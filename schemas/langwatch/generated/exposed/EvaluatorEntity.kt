package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class EvaluatorEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, EvaluatorEntity>(EvaluatorTable)

    var projectId by EvaluatorTable.projectId
    var name by EvaluatorTable.name
    var slug by EvaluatorTable.slug
    var type by EvaluatorTable.type
    var config by EvaluatorTable.config
    var workflowId by EvaluatorTable.workflowId
    var copiedFromEvaluatorId by EvaluatorTable.copiedFromEvaluatorId
    var archivedAt by EvaluatorTable.archivedAt
    var createdAt by EvaluatorTable.createdAt
    var updatedAt by EvaluatorTable.updatedAt
    var createdById by EvaluatorTable.createdById
    var updatedById by EvaluatorTable.updatedById

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(EvaluatorTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
