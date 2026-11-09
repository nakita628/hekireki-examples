package jitsu.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass
import java.util.UUID

class TaskLogEntity(
    id: EntityID<UUID>,
) : Entity<UUID>(id) {
    companion object : EntityClass<UUID, TaskLogEntity>(TaskLogTable)

    var level by TaskLogTable.level
    var logger by TaskLogTable.logger
    var message by TaskLogTable.message
    var syncId by TaskLogTable.syncId
    var taskId by TaskLogTable.taskId
    var timestamp by TaskLogTable.timestamp
}
