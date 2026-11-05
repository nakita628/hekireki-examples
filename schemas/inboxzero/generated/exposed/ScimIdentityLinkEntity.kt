package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class ScimIdentityLinkEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ScimIdentityLinkEntity>(ScimIdentityLinkTable)

    var connectionId by ScimIdentityLinkTable.connectionId
    var externalId by ScimIdentityLinkTable.externalId
    var userId by ScimIdentityLinkTable.userId
    var user by UserEntity referencedOn ScimIdentityLinkTable.userId
}
