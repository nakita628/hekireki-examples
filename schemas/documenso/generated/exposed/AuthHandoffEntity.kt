package documenso.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class AuthHandoffEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, AuthHandoffEntity>(AuthHandoffTable)

    var type by AuthHandoffTable.type
    var payload by AuthHandoffTable.payload
    var createdAt by AuthHandoffTable.createdAt
    var expiresAt by AuthHandoffTable.expiresAt
}
