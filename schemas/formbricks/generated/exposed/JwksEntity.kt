package formbricks.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class JwksEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, JwksEntity>(JwksTable)

    var publicKey by JwksTable.publicKey
    var privateKey by JwksTable.privateKey
    var createdAt by JwksTable.createdAt
    var expiresAt by JwksTable.expiresAt
    var alg by JwksTable.alg
    var crv by JwksTable.crv
}
