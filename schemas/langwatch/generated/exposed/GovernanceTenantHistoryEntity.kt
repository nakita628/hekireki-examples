package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class GovernanceTenantHistoryEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, GovernanceTenantHistoryEntity>(GovernanceTenantHistoryTable)

    var organizationId by GovernanceTenantHistoryTable.organizationId
    var tenantId by GovernanceTenantHistoryTable.tenantId
    var firstUsedAt by GovernanceTenantHistoryTable.firstUsedAt
    var lastUsedAt by GovernanceTenantHistoryTable.lastUsedAt
}
