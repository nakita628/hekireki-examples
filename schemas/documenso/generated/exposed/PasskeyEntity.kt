package documenso.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class PasskeyEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, PasskeyEntity>(PasskeyTable)

    var userId by PasskeyTable.userId
    var name by PasskeyTable.name
    var createdAt by PasskeyTable.createdAt
    var updatedAt by PasskeyTable.updatedAt
    var lastUsedAt by PasskeyTable.lastUsedAt
    var credentialId by PasskeyTable.credentialId
    var credentialPublicKey by PasskeyTable.credentialPublicKey
    var counter by PasskeyTable.counter
    var credentialDeviceType by PasskeyTable.credentialDeviceType
    var credentialBackedUp by PasskeyTable.credentialBackedUp
    var transports by PasskeyTable.transports
    var user by UserEntity referencedOn PasskeyTable.userId
}
