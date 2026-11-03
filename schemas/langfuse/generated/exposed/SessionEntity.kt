package langfuse.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class SessionEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, SessionEntity>(SessionTable)

    var sessionToken by SessionTable.sessionToken
    var userId by SessionTable.userId
    var expires by SessionTable.expires
    var user by UserEntity referencedOn SessionTable.userId
}
