package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class UserErasureEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, UserErasureEntity>(UserErasureTable)

    var accountCreatedAt by UserErasureTable.accountCreatedAt
    var erasedAt by UserErasureTable.erasedAt
}
