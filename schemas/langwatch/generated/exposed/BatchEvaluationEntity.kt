package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class BatchEvaluationEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, BatchEvaluationEntity>(BatchEvaluationTable)

    var experimentId by BatchEvaluationTable.experimentId
    var projectId by BatchEvaluationTable.projectId
    var data by BatchEvaluationTable.data
    var status by BatchEvaluationTable.status
    var score by BatchEvaluationTable.score
    var label by BatchEvaluationTable.label
    var passed by BatchEvaluationTable.passed
    var details by BatchEvaluationTable.details
    var cost by BatchEvaluationTable.cost
    var datasetSlug by BatchEvaluationTable.datasetSlug
    var datasetId by BatchEvaluationTable.datasetId
    var evaluation by BatchEvaluationTable.evaluation
    var createdAt by BatchEvaluationTable.createdAt
    var updatedAt by BatchEvaluationTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(BatchEvaluationTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
