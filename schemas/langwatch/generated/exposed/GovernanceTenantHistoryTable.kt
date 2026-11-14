package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable

object GovernanceTenantHistoryTable : IdTable<String>("\"GovernanceTenantHistory\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val organizationId = text("organizationId")
    val tenantId = text("tenantId")
    val firstUsedAt = pgTimestamp("firstUsedAt", 3)
    val lastUsedAt = pgTimestamp("lastUsedAt", 3)
    override val primaryKey = PrimaryKey(id, name = "\"GovernanceTenantHistory_pkey\"")

    init {
        index("\"GovernanceTenantHistory_organizationId_idx\"", false, organizationId)
        uniqueIndex("\"GovernanceTenantHistory_organizationId_tenantId_key\"", organizationId, tenantId)
    }
}
