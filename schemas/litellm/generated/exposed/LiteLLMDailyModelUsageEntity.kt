package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import java.time.Instant
import java.time.temporal.ChronoUnit

class LiteLLMDailyModelUsageEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<LiteLLMDailyModelUsageEntity>(LiteLLMDailyModelUsageTable)

    val date by LiteLLMDailyModelUsageTable.date
    val modelGroup by LiteLLMDailyModelUsageTable.modelGroup
    val model by LiteLLMDailyModelUsageTable.model
    val customLlmProvider by LiteLLMDailyModelUsageTable.customLlmProvider
    val taskType by LiteLLMDailyModelUsageTable.taskType
    var spend by LiteLLMDailyModelUsageTable.spend
    var promptTokens by LiteLLMDailyModelUsageTable.promptTokens
    var completionTokens by LiteLLMDailyModelUsageTable.completionTokens
    var requestCount by LiteLLMDailyModelUsageTable.requestCount
    var successfulRequests by LiteLLMDailyModelUsageTable.successfulRequests
    var failedRequests by LiteLLMDailyModelUsageTable.failedRequests
    var createdAt by LiteLLMDailyModelUsageTable.createdAt
    var updatedAt by LiteLLMDailyModelUsageTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(LiteLLMDailyModelUsageTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
