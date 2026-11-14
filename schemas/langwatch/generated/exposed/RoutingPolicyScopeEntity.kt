package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class RoutingPolicyScopeEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, RoutingPolicyScopeEntity>(RoutingPolicyScopeTable)

    var routingPolicyId by RoutingPolicyScopeTable.routingPolicyId
    var scopeType by RoutingPolicyScopeTable.scopeType
    var scopeId by RoutingPolicyScopeTable.scopeId
    var createdAt by RoutingPolicyScopeTable.createdAt
}
