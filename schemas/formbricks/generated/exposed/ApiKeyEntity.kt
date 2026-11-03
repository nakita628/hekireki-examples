package formbricks.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class ApiKeyEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ApiKeyEntity>(ApiKeyTable)

    var createdAt by ApiKeyTable.createdAt
    var createdBy by ApiKeyTable.createdBy
    var lastUsedAt by ApiKeyTable.lastUsedAt
    var label by ApiKeyTable.label
    var hashedKey by ApiKeyTable.hashedKey
    var lookupHash by ApiKeyTable.lookupHash
    var organizationId by ApiKeyTable.organizationId
    var organization by OrganizationEntity referencedOn ApiKeyTable.organizationId
    val apiKeyWorkspaces by ApiKeyWorkspaceEntity referrersOn ApiKeyWorkspaceTable.apiKeyId
    var organizationAccess by ApiKeyTable.organizationAccess
}
