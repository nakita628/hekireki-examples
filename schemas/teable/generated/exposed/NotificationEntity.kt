package teable.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class NotificationEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, NotificationEntity>(NotificationTable)

    var fromUserId by NotificationTable.fromUserId
    var toUserId by NotificationTable.toUserId
    var type by NotificationTable.type
    var message by NotificationTable.message
    var messageI18n by NotificationTable.messageI18n
    var severity by NotificationTable.severity
    var urlPath by NotificationTable.urlPath
    var isRead by NotificationTable.isRead
    var createdTime by NotificationTable.createdTime
    var createdBy by NotificationTable.createdBy
}
