package jitsu.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass

class NotificationStateEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<NotificationStateEntity>(NotificationStateTable)

    var workspaceId by NotificationStateTable.workspaceId
    val actorId by NotificationStateTable.actorId
    val type by NotificationStateTable.type
    val tableName by NotificationStateTable.tableName1
    val channelId by NotificationStateTable.channelId
    var flappingSince by NotificationStateTable.flappingSince
    var lastNotification by NotificationStateTable.lastNotification
    var statusChangeId by NotificationStateTable.statusChangeId
    var error by NotificationStateTable.error
}
