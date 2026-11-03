package teable.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class OAuthAppAuthorizedEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, OAuthAppAuthorizedEntity>(OAuthAppAuthorizedTable)

    var clientId by OAuthAppAuthorizedTable.clientId
    var userId by OAuthAppAuthorizedTable.userId
    var authorizedTime by OAuthAppAuthorizedTable.authorizedTime
    var oAuthApp by OAuthAppEntity referencedOn OAuthAppAuthorizedTable.clientId
}
