package jitsu.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class SourceTaskEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, SourceTaskEntity>(SourceTaskTable)

    var syncId by SourceTaskTable.syncId
    var `package` by SourceTaskTable.`package`
    var version by SourceTaskTable.version
    var startedAt by SourceTaskTable.startedAt
    var updatedAt by SourceTaskTable.updatedAt
    var startedBy by SourceTaskTable.startedBy
    var status by SourceTaskTable.status
    var description by SourceTaskTable.description
    var metrics by SourceTaskTable.metrics
    var error by SourceTaskTable.error
}
