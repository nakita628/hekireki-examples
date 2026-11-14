package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class AnomalyRuleEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, AnomalyRuleEntity>(AnomalyRuleTable)

    var organizationId by AnomalyRuleTable.organizationId
    var scope by AnomalyRuleTable.scope
    var scopeId by AnomalyRuleTable.scopeId
    var name by AnomalyRuleTable.name
    var description by AnomalyRuleTable.description
    var severity by AnomalyRuleTable.severity
    var ruleType by AnomalyRuleTable.ruleType
    var thresholdConfig by AnomalyRuleTable.thresholdConfig
    var destinationConfig by AnomalyRuleTable.destinationConfig
    var status by AnomalyRuleTable.status
    var archivedAt by AnomalyRuleTable.archivedAt
    var createdAt by AnomalyRuleTable.createdAt
    var updatedAt by AnomalyRuleTable.updatedAt
    var createdById by AnomalyRuleTable.createdById

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(AnomalyRuleTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
