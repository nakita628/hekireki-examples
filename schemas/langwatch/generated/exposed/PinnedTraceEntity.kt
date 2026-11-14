package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class PinnedTraceEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, PinnedTraceEntity>(PinnedTraceTable)

    var projectId by PinnedTraceTable.projectId
    var traceId by PinnedTraceTable.traceId
    var userId by PinnedTraceTable.userId
    var source by PinnedTraceTable.source1
    var reason by PinnedTraceTable.reason
    var createdAt by PinnedTraceTable.createdAt
}
