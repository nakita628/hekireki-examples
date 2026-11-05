package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class EmailTokenEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, EmailTokenEntity>(EmailTokenTable)

    var createdAt by EmailTokenTable.createdAt
    var token by EmailTokenTable.token
    var expiresAt by EmailTokenTable.expiresAt
    var emailAccountId by EmailTokenTable.emailAccountId
    var emailAccount by EmailAccountEntity referencedOn EmailTokenTable.emailAccountId
}
