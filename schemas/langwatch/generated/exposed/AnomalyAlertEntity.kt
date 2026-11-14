package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class AnomalyAlertEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, AnomalyAlertEntity>(AnomalyAlertTable)

    var organizationId by AnomalyAlertTable.organizationId
    var ruleId by AnomalyAlertTable.ruleId
    var severity by AnomalyAlertTable.severity
    var ruleName by AnomalyAlertTable.ruleName
    var ruleType by AnomalyAlertTable.ruleType
    var triggerWindowStart by AnomalyAlertTable.triggerWindowStart
    var triggerWindowEnd by AnomalyAlertTable.triggerWindowEnd
    var triggerSpendUsd by AnomalyAlertTable.triggerSpendUsd
    var triggerEventCount by AnomalyAlertTable.triggerEventCount
    var detail by AnomalyAlertTable.detail
    var state by AnomalyAlertTable.state
    var destinationStatus by AnomalyAlertTable.destinationStatus
    var detectedAt by AnomalyAlertTable.detectedAt
    var acknowledgedAt by AnomalyAlertTable.acknowledgedAt
    var resolvedAt by AnomalyAlertTable.resolvedAt
}
