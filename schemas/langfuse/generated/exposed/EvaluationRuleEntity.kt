package langfuse.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class EvaluationRuleEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, EvaluationRuleEntity>(EvaluationRuleTable)

    var createdAt by EvaluationRuleTable.createdAt
    var updatedAt by EvaluationRuleTable.updatedAt
    var projectId by EvaluationRuleTable.projectId
    var project by ProjectEntity referencedOn EvaluationRuleTable.projectId
    var createdByUserId by EvaluationRuleTable.createdByUserId
    var createdByUser by UserEntity optionalReferencedOn EvaluationRuleTable.createdByUserId
    var name by EvaluationRuleTable.name
    var status by EvaluationRuleTable.status
    var targetObject by EvaluationRuleTable.targetObject
    var filter by EvaluationRuleTable.filter
    var sampling by EvaluationRuleTable.sampling
    var delay by EvaluationRuleTable.delay
    var idleTime by EvaluationRuleTable.idleTime
    var timeScope by EvaluationRuleTable.timeScope
    val assignments by EvaluationRuleEvaluatorAssignmentEntity referrersOn EvaluationRuleEvaluatorAssignmentTable.evaluationRuleId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(EvaluationRuleTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
