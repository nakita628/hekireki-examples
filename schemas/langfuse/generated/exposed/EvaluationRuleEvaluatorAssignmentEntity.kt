package langfuse.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class EvaluationRuleEvaluatorAssignmentEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, EvaluationRuleEvaluatorAssignmentEntity>(EvaluationRuleEvaluatorAssignmentTable)

    var createdAt by EvaluationRuleEvaluatorAssignmentTable.createdAt
    var updatedAt by EvaluationRuleEvaluatorAssignmentTable.updatedAt
    var projectId by EvaluationRuleEvaluatorAssignmentTable.projectId
    var project by ProjectEntity referencedOn EvaluationRuleEvaluatorAssignmentTable.projectId
    var evaluationRuleId by EvaluationRuleEvaluatorAssignmentTable.evaluationRuleId
    var evaluationRule by EvaluationRuleEntity referencedOn EvaluationRuleEvaluatorAssignmentTable.evaluationRuleId
    var evaluatorId by EvaluationRuleEvaluatorAssignmentTable.evaluatorId
    var evaluator by EvaluatorEntity referencedOn EvaluationRuleEvaluatorAssignmentTable.evaluatorId
    var variableMapping by EvaluationRuleEvaluatorAssignmentTable.variableMapping

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(EvaluationRuleEvaluatorAssignmentTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
