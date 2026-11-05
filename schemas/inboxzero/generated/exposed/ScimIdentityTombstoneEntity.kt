package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class ScimIdentityTombstoneEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ScimIdentityTombstoneEntity>(ScimIdentityTombstoneTable)

    var connectionId by ScimIdentityTombstoneTable.connectionId
    var provisioningDomainId by ScimIdentityTombstoneTable.provisioningDomainId
    var externalId by ScimIdentityTombstoneTable.externalId
    var externalIdKey by ScimIdentityTombstoneTable.externalIdKey
    var userId by ScimIdentityTombstoneTable.userId
    var user by UserEntity referencedOn ScimIdentityTombstoneTable.userId
    var profile by ScimIdentityTombstoneTable.profile
    var deletedAt by ScimIdentityTombstoneTable.deletedAt
}
