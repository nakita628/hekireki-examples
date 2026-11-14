package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class EvaluatorVersionLabelEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, EvaluatorVersionLabelEntity>(EvaluatorVersionLabelTable)

    var projectId by EvaluatorVersionLabelTable.projectId
    var evaluatorId by EvaluatorVersionLabelTable.evaluatorId
    var label by EvaluatorVersionLabelTable.label
    var versionId by EvaluatorVersionLabelTable.versionId
    var createdById by EvaluatorVersionLabelTable.createdById
    var updatedById by EvaluatorVersionLabelTable.updatedById
    var createdAt by EvaluatorVersionLabelTable.createdAt
    var updatedAt by EvaluatorVersionLabelTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(EvaluatorVersionLabelTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
