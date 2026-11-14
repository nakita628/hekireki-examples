package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class RoutingPolicyEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, RoutingPolicyEntity>(RoutingPolicyTable)

    var organizationId by RoutingPolicyTable.organizationId
    var name by RoutingPolicyTable.name
    var description by RoutingPolicyTable.description
    var modelProviderIds by RoutingPolicyTable.modelProviderIds
    var modelAliases by RoutingPolicyTable.modelAliases
    var defaultModel by RoutingPolicyTable.defaultModel
    var policyRules by RoutingPolicyTable.policyRules
    var isDefault by RoutingPolicyTable.isDefault
    var createdAt by RoutingPolicyTable.createdAt
    var updatedAt by RoutingPolicyTable.updatedAt
    var createdById by RoutingPolicyTable.createdById
    var updatedById by RoutingPolicyTable.updatedById

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(RoutingPolicyTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
