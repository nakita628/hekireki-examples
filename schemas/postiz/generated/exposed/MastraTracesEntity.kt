package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class MastraTracesEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, MastraTracesEntity>(MastraTracesTable)

    var parentSpanId by MastraTracesTable.parentSpanId
    var name by MastraTracesTable.name
    var traceId by MastraTracesTable.traceId
    var scope by MastraTracesTable.scope
    var kind by MastraTracesTable.kind
    var attributes by MastraTracesTable.attributes
    var status by MastraTracesTable.status
    var events by MastraTracesTable.events
    var links by MastraTracesTable.links
    var other by MastraTracesTable.other
    var startTime by MastraTracesTable.startTime
    var endTime by MastraTracesTable.endTime
    var createdAt by MastraTracesTable.createdAt
    var createdAtZ by MastraTracesTable.createdAtZ
}
