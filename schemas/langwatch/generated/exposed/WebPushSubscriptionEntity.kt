package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class WebPushSubscriptionEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, WebPushSubscriptionEntity>(WebPushSubscriptionTable)

    var userId by WebPushSubscriptionTable.userId
    var endpoint by WebPushSubscriptionTable.endpoint
    var p256dh by WebPushSubscriptionTable.p256dh
    var auth by WebPushSubscriptionTable.auth
    var userAgent by WebPushSubscriptionTable.userAgent
    var createdAt by WebPushSubscriptionTable.createdAt
    var lastSuccessAt by WebPushSubscriptionTable.lastSuccessAt
}
