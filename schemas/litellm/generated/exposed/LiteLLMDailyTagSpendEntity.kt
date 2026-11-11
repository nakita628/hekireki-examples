package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class LiteLLMDailyTagSpendEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LiteLLMDailyTagSpendEntity>(LiteLLMDailyTagSpendTable)

    var requestId by LiteLLMDailyTagSpendTable.requestId
    var tag by LiteLLMDailyTagSpendTable.tag
    var date by LiteLLMDailyTagSpendTable.date
    var apiKey by LiteLLMDailyTagSpendTable.apiKey
    var model by LiteLLMDailyTagSpendTable.model
    var modelGroup by LiteLLMDailyTagSpendTable.modelGroup
    var customLlmProvider by LiteLLMDailyTagSpendTable.customLlmProvider
    var mcpNamespacedToolName by LiteLLMDailyTagSpendTable.mcpNamespacedToolName
    var endpoint by LiteLLMDailyTagSpendTable.endpoint
    var promptTokens by LiteLLMDailyTagSpendTable.promptTokens
    var completionTokens by LiteLLMDailyTagSpendTable.completionTokens
    var cacheReadInputTokens by LiteLLMDailyTagSpendTable.cacheReadInputTokens
    var cacheCreationInputTokens by LiteLLMDailyTagSpendTable.cacheCreationInputTokens
    var compressionSavedTokens by LiteLLMDailyTagSpendTable.compressionSavedTokens
    var compressionSavingsSpend by LiteLLMDailyTagSpendTable.compressionSavingsSpend
    var promptCachingSavingsSpend by LiteLLMDailyTagSpendTable.promptCachingSavingsSpend
    var gatewayInjectedCachingSavingsSpend by LiteLLMDailyTagSpendTable.gatewayInjectedCachingSavingsSpend
    var autorouterSavingsSpend by LiteLLMDailyTagSpendTable.autorouterSavingsSpend
    var guardrailSpend by LiteLLMDailyTagSpendTable.guardrailSpend
    var spend by LiteLLMDailyTagSpendTable.spend
    var apiRequests by LiteLLMDailyTagSpendTable.apiRequests
    var successfulRequests by LiteLLMDailyTagSpendTable.successfulRequests
    var failedRequests by LiteLLMDailyTagSpendTable.failedRequests
    var totalResponseTimeMs by LiteLLMDailyTagSpendTable.totalResponseTimeMs
    var timedRequests by LiteLLMDailyTagSpendTable.timedRequests
    var createdAt by LiteLLMDailyTagSpendTable.createdAt
    var updatedAt by LiteLLMDailyTagSpendTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(LiteLLMDailyTagSpendTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
