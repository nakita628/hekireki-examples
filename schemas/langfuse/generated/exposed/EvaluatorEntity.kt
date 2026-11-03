package langfuse.models

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

    var createdAt by EvaluatorTable.createdAt
    var updatedAt by EvaluatorTable.updatedAt
    var projectId by EvaluatorTable.projectId
    var project by ProjectEntity referencedOn EvaluatorTable.projectId
    var name by EvaluatorTable.name
    var type by EvaluatorTable.type
    var isBuiltIn by EvaluatorTable.isBuiltIn
    var description by EvaluatorTable.description
    var createdByUserId by EvaluatorTable.createdByUserId
    var createdByUser by UserEntity optionalReferencedOn EvaluatorTable.createdByUserId
    var blockedAt by EvaluatorTable.blockedAt
    var blockReason by EvaluatorTable.blockReason
    var blockMessage by EvaluatorTable.blockMessage
    val versions by EvaluatorVersionEntity referrersOn EvaluatorVersionTable.evaluatorId
    val assignments by EvaluationRuleEvaluatorAssignmentEntity referrersOn EvaluationRuleEvaluatorAssignmentTable.evaluatorId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(EvaluatorTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
