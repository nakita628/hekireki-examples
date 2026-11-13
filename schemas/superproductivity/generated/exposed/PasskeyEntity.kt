package superproductivity.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class PasskeyEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, PasskeyEntity>(PasskeyTable)

    var credentialId by PasskeyTable.credentialId
    var publicKey by PasskeyTable.publicKey
    var counter by PasskeyTable.counter
    var transports by PasskeyTable.transports
    var createdAt by PasskeyTable.createdAt
    var lastUsedAt by PasskeyTable.lastUsedAt
    var userId by PasskeyTable.userId
    var user by UserEntity referencedOn PasskeyTable.userId
}
