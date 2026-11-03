package typebot.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class UserCredentialsEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, UserCredentialsEntity>(UserCredentialsTable)

    var createdAt by UserCredentialsTable.createdAt
    var userId by UserCredentialsTable.userId
    var data by UserCredentialsTable.data
    var name by UserCredentialsTable.name
    var type by UserCredentialsTable.type
    var iv by UserCredentialsTable.iv
    var user by UserEntity referencedOn UserCredentialsTable.userId
}
