package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class OauthClientResourceEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, OauthClientResourceEntity>(OauthClientResourceTable)

    var clientId by OauthClientResourceTable.clientId
    var client by OauthClientEntity referencedOn OauthClientResourceTable.clientId
    var resourceId by OauthClientResourceTable.resourceId
    var oauthResource by OauthResourceEntity referencedOn OauthClientResourceTable.resourceId
    var metadata by OauthClientResourceTable.metadata
    var createdAt by OauthClientResourceTable.createdAt
}
