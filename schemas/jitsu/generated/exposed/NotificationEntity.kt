package jitsu.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass
import java.util.UUID

class NotificationEntity(
    id: EntityID<UUID>,
) : Entity<UUID>(id) {
    companion object : EntityClass<UUID, NotificationEntity>(NotificationTable)

    var workspaceId by NotificationTable.workspaceId
    var actorId by NotificationTable.actorId
    var type by NotificationTable.type
    var tableName by NotificationTable.tableName1
    var channelId by NotificationTable.channelId
    var statusChangeId by NotificationTable.statusChangeId
    var timestamp by NotificationTable.timestamp
    var status by NotificationTable.status
    var error by NotificationTable.error
}
