package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class GatewayCacheRuleEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, GatewayCacheRuleEntity>(GatewayCacheRuleTable)

    var organizationId by GatewayCacheRuleTable.organizationId
    var name by GatewayCacheRuleTable.name
    var description by GatewayCacheRuleTable.description
    var priority by GatewayCacheRuleTable.priority
    var enabled by GatewayCacheRuleTable.enabled
    var matchers by GatewayCacheRuleTable.matchers
    var action by GatewayCacheRuleTable.action
    var modeEnum by GatewayCacheRuleTable.modeEnum
    var archivedAt by GatewayCacheRuleTable.archivedAt
    var createdAt by GatewayCacheRuleTable.createdAt
    var updatedAt by GatewayCacheRuleTable.updatedAt
    var createdById by GatewayCacheRuleTable.createdById

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(GatewayCacheRuleTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
