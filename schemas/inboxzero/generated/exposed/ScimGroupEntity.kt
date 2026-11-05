package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class ScimGroupEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ScimGroupEntity>(ScimGroupTable)

    var connectionId by ScimGroupTable.connectionId
    var provisioningDomainId by ScimGroupTable.provisioningDomainId
    var revision by ScimGroupTable.revision
    var displayName by ScimGroupTable.displayName
    var displayNameKey by ScimGroupTable.displayNameKey
    var externalId by ScimGroupTable.externalId
    var externalIdKey by ScimGroupTable.externalIdKey
    var orderKey by ScimGroupTable.orderKey
    var createdAt by ScimGroupTable.createdAt
    var updatedAt by ScimGroupTable.updatedAt
    val scimGroupMembers by ScimGroupMemberEntity referrersOn ScimGroupMemberTable.groupId
}
