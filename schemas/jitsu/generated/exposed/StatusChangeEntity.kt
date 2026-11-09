package jitsu.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class StatusChangeEntity(
    id: EntityID<Long>,
) : Entity<Long>(id) {
    companion object : EntityClass<Long, StatusChangeEntity>(StatusChangeTable)

    var workspaceId by StatusChangeTable.workspaceId
    var actorId by StatusChangeTable.actorId
    var type by StatusChangeTable.type
    var tableName by StatusChangeTable.tableName1
    var timestamp by StatusChangeTable.timestamp
    var status by StatusChangeTable.status
    var startedAt by StatusChangeTable.startedAt
    var counts by StatusChangeTable.counts
    var description by StatusChangeTable.description
    var queueSize by StatusChangeTable.queueSize
}
