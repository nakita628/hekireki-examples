package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class GatewayChangeEventEntity(
    id: EntityID<Long>,
) : Entity<Long>(id) {
    companion object : EntityClass<Long, GatewayChangeEventEntity>(GatewayChangeEventTable)

    var organizationId by GatewayChangeEventTable.organizationId
    var kind by GatewayChangeEventTable.kind
    var virtualKeyId by GatewayChangeEventTable.virtualKeyId
    var budgetId by GatewayChangeEventTable.budgetId
    var modelProviderId by GatewayChangeEventTable.modelProviderId
    var projectId by GatewayChangeEventTable.projectId
    var payload by GatewayChangeEventTable.payload
    var createdAt by GatewayChangeEventTable.createdAt
}
