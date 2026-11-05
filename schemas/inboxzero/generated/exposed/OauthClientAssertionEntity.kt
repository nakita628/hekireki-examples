package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class OauthClientAssertionEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, OauthClientAssertionEntity>(OauthClientAssertionTable)

    var expiresAt by OauthClientAssertionTable.expiresAt
}
