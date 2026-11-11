package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass

class LiteLLMLensTraceSignalEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<LiteLLMLensTraceSignalEntity>(LiteLLMLensTraceSignalTable)

    val traceId by LiteLLMLensTraceSignalTable.traceId
    val traceRef by LiteLLMLensTraceSignalTable.traceRef
    var configKey by LiteLLMLensTraceSignalTable.configKey
    var spanCount by LiteLLMLensTraceSignalTable.spanCount
    var claimedUntil by LiteLLMLensTraceSignalTable.claimedUntil
    var classifiedAt by LiteLLMLensTraceSignalTable.classifiedAt
    var data by LiteLLMLensTraceSignalTable.data
}
