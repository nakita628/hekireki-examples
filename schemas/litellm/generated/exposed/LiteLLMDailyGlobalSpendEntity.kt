package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class LiteLLMDailyGlobalSpendEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LiteLLMDailyGlobalSpendEntity>(LiteLLMDailyGlobalSpendTable)

    var date by LiteLLMDailyGlobalSpendTable.date
    var model by LiteLLMDailyGlobalSpendTable.model
    var modelGroup by LiteLLMDailyGlobalSpendTable.modelGroup
    var customLlmProvider by LiteLLMDailyGlobalSpendTable.customLlmProvider
    var mcpNamespacedToolName by LiteLLMDailyGlobalSpendTable.mcpNamespacedToolName
    var endpoint by LiteLLMDailyGlobalSpendTable.endpoint
    var promptTokens by LiteLLMDailyGlobalSpendTable.promptTokens
    var completionTokens by LiteLLMDailyGlobalSpendTable.completionTokens
    var cacheReadInputTokens by LiteLLMDailyGlobalSpendTable.cacheReadInputTokens
    var cacheCreationInputTokens by LiteLLMDailyGlobalSpendTable.cacheCreationInputTokens
    var compressionSavedTokens by LiteLLMDailyGlobalSpendTable.compressionSavedTokens
    var compressionSavingsSpend by LiteLLMDailyGlobalSpendTable.compressionSavingsSpend
    var promptCachingSavingsSpend by LiteLLMDailyGlobalSpendTable.promptCachingSavingsSpend
    var gatewayInjectedCachingSavingsSpend by LiteLLMDailyGlobalSpendTable.gatewayInjectedCachingSavingsSpend
    var autorouterSavingsSpend by LiteLLMDailyGlobalSpendTable.autorouterSavingsSpend
    var guardrailSpend by LiteLLMDailyGlobalSpendTable.guardrailSpend
    var spend by LiteLLMDailyGlobalSpendTable.spend
    var apiRequests by LiteLLMDailyGlobalSpendTable.apiRequests
    var successfulRequests by LiteLLMDailyGlobalSpendTable.successfulRequests
    var failedRequests by LiteLLMDailyGlobalSpendTable.failedRequests
    var totalResponseTimeMs by LiteLLMDailyGlobalSpendTable.totalResponseTimeMs
    var timedRequests by LiteLLMDailyGlobalSpendTable.timedRequests
    var createdAt by LiteLLMDailyGlobalSpendTable.createdAt
    var updatedAt by LiteLLMDailyGlobalSpendTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(LiteLLMDailyGlobalSpendTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
