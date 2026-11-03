package documenso.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class AnonymousVerificationTokenEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, AnonymousVerificationTokenEntity>(AnonymousVerificationTokenTable)

    var token by AnonymousVerificationTokenTable.token
    var expiresAt by AnonymousVerificationTokenTable.expiresAt
    var createdAt by AnonymousVerificationTokenTable.createdAt
}
