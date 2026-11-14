package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass

class NurturingProjectTraceDayEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<NurturingProjectTraceDayEntity>(NurturingProjectTraceDayTable)

    val projectId by NurturingProjectTraceDayTable.projectId
    val day by NurturingProjectTraceDayTable.day
    var traceCount by NurturingProjectTraceDayTable.traceCount
    var errorCount by NurturingProjectTraceDayTable.errorCount
    var lastTraceAt by NurturingProjectTraceDayTable.lastTraceAt
    var lastFailedTraceAt by NurturingProjectTraceDayTable.lastFailedTraceAt
}
