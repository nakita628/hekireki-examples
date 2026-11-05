package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class ScimProjectionGrantEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ScimProjectionGrantEntity>(ScimProjectionGrantTable)

    var connectionId by ScimProjectionGrantTable.connectionId
    var provisioningDomainId by ScimProjectionGrantTable.provisioningDomainId
    var scimUserId by ScimProjectionGrantTable.scimUserId
    var scimUser by ScimUserEntity referencedOn ScimProjectionGrantTable.scimUserId
    var userId by ScimProjectionGrantTable.userId
    var user by UserEntity referencedOn ScimProjectionGrantTable.userId
    var sourceKind by ScimProjectionGrantTable.sourceKind
    var sourceId by ScimProjectionGrantTable.sourceId
    var sourceValue by ScimProjectionGrantTable.sourceValue
    var role by ScimProjectionGrantTable.role
    var grantKey by ScimProjectionGrantTable.grantKey
    var createdAt by ScimProjectionGrantTable.createdAt
    var updatedAt by ScimProjectionGrantTable.updatedAt
}
