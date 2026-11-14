package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class WebPushVapidKeyEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, WebPushVapidKeyEntity>(WebPushVapidKeyTable)

    var publicKey by WebPushVapidKeyTable.publicKey
    var privateKeyEncrypted by WebPushVapidKeyTable.privateKeyEncrypted
    var createdAt by WebPushVapidKeyTable.createdAt
}
