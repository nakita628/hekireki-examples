package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import java.time.Instant
import java.time.temporal.ChronoUnit

class LiteLLMAdaptiveRouterStateEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<LiteLLMAdaptiveRouterStateEntity>(LiteLLMAdaptiveRouterStateTable)

    val routerName by LiteLLMAdaptiveRouterStateTable.routerName
    val requestType by LiteLLMAdaptiveRouterStateTable.requestType
    val modelName by LiteLLMAdaptiveRouterStateTable.modelName
    var alpha by LiteLLMAdaptiveRouterStateTable.alpha
    var beta by LiteLLMAdaptiveRouterStateTable.beta
    var totalSamples by LiteLLMAdaptiveRouterStateTable.totalSamples
    var lastUpdatedAt by LiteLLMAdaptiveRouterStateTable.lastUpdatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(LiteLLMAdaptiveRouterStateTable.lastUpdatedAt)) {
                lastUpdatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
