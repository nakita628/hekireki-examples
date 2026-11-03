package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class NotificationsSubscriptionsEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, NotificationsSubscriptionsEntity>(NotificationsSubscriptionsTable)

    var userId by NotificationsSubscriptionsTable.userId
    var user by UserEntity referencedOn NotificationsSubscriptionsTable.userId
    var subscription by NotificationsSubscriptionsTable.subscription
}
