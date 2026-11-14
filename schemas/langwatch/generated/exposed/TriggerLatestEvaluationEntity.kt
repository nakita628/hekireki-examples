package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class TriggerLatestEvaluationEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, TriggerLatestEvaluationEntity>(TriggerLatestEvaluationTable)

    var projectId by TriggerLatestEvaluationTable.projectId
    var evaluatedAt by TriggerLatestEvaluationTable.evaluatedAt
    var verdict by TriggerLatestEvaluationTable.verdict
    var observedValue by TriggerLatestEvaluationTable.observedValue
    var threshold by TriggerLatestEvaluationTable.threshold
    var operator by TriggerLatestEvaluationTable.operator
    var timePeriodMinutes by TriggerLatestEvaluationTable.timePeriodMinutes
    var skipCode by TriggerLatestEvaluationTable.skipCode
    var updatedAt by TriggerLatestEvaluationTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(TriggerLatestEvaluationTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
