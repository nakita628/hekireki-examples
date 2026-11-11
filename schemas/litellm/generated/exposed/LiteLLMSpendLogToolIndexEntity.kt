package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass

class LiteLLMSpendLogToolIndexEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<LiteLLMSpendLogToolIndexEntity>(LiteLLMSpendLogToolIndexTable)

    val requestId by LiteLLMSpendLogToolIndexTable.requestId
    val toolName by LiteLLMSpendLogToolIndexTable.toolName
    var startTime by LiteLLMSpendLogToolIndexTable.startTime
}
