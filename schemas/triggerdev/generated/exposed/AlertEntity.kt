package triggerdev.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class AlertEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, AlertEntity>(AlertTable)

    var organizationId by AlertTable.organizationId
    var projectId by AlertTable.projectId
    var environmentId by AlertTable.environmentId
    var capabilityKey by AlertTable.capabilityKey
    var params by AlertTable.params
    var condition by AlertTable.condition
    var query by AlertTable.query
    var expiresAt by AlertTable.expiresAt
    var lifecycle by AlertTable.lifecycle
    var attention by AlertTable.attention
    var observedCondition by AlertTable.observedCondition
    var observedConditionChangedAt by AlertTable.observedConditionChangedAt
    var pendingMetSince by AlertTable.pendingMetSince
    var evaluatorState by AlertTable.evaluatorState
    var evaluationLeaseId by AlertTable.evaluationLeaseId
    var evaluationLeaseUntil by AlertTable.evaluationLeaseUntil
    var lastEvaluatedAt by AlertTable.lastEvaluatedAt
    var nextEvaluationAt by AlertTable.nextEvaluationAt
    var notifiers by AlertTable.notifiers
    var name by AlertTable.name
    var createdBy by AlertTable.createdBy
    var createdAt by AlertTable.createdAt
    var updatedAt by AlertTable.updatedAt
    var resolvedAt by AlertTable.resolvedAt
    var resolvedBy by AlertTable.resolvedBy
    var stoppedAt by AlertTable.stoppedAt
    var stoppedBy by AlertTable.stoppedBy
    var lastOccurrenceAt by AlertTable.lastOccurrenceAt
    var occurrencesSinceRaise by AlertTable.occurrencesSinceRaise
    var occurrencesTotal by AlertTable.occurrencesTotal
}
