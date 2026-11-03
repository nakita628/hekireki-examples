package teable.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class AccountEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, AccountEntity>(AccountTable)

    var userId by AccountTable.userId
    var type by AccountTable.type
    var provider by AccountTable.provider
    var providerId by AccountTable.providerId
    var connectionId by AccountTable.connectionId
    var createdTime by AccountTable.createdTime
    var user by UserEntity referencedOn AccountTable.userId
}
