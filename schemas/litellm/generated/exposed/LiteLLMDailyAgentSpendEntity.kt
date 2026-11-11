package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class LiteLLMDailyAgentSpendEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LiteLLMDailyAgentSpendEntity>(LiteLLMDailyAgentSpendTable)

    var agentId by LiteLLMDailyAgentSpendTable.agentId
    var date by LiteLLMDailyAgentSpendTable.date
    var apiKey by LiteLLMDailyAgentSpendTable.apiKey
    var model by LiteLLMDailyAgentSpendTable.model
    var modelGroup by LiteLLMDailyAgentSpendTable.modelGroup
    var customLlmProvider by LiteLLMDailyAgentSpendTable.customLlmProvider
    var mcpNamespacedToolName by LiteLLMDailyAgentSpendTable.mcpNamespacedToolName
    var endpoint by LiteLLMDailyAgentSpendTable.endpoint
    var promptTokens by LiteLLMDailyAgentSpendTable.promptTokens
    var completionTokens by LiteLLMDailyAgentSpendTable.completionTokens
    var cacheReadInputTokens by LiteLLMDailyAgentSpendTable.cacheReadInputTokens
    var cacheCreationInputTokens by LiteLLMDailyAgentSpendTable.cacheCreationInputTokens
    var compressionSavedTokens by LiteLLMDailyAgentSpendTable.compressionSavedTokens
    var compressionSavingsSpend by LiteLLMDailyAgentSpendTable.compressionSavingsSpend
    var promptCachingSavingsSpend by LiteLLMDailyAgentSpendTable.promptCachingSavingsSpend
    var gatewayInjectedCachingSavingsSpend by LiteLLMDailyAgentSpendTable.gatewayInjectedCachingSavingsSpend
    var autorouterSavingsSpend by LiteLLMDailyAgentSpendTable.autorouterSavingsSpend
    var guardrailSpend by LiteLLMDailyAgentSpendTable.guardrailSpend
    var spend by LiteLLMDailyAgentSpendTable.spend
    var apiRequests by LiteLLMDailyAgentSpendTable.apiRequests
    var successfulRequests by LiteLLMDailyAgentSpendTable.successfulRequests
    var failedRequests by LiteLLMDailyAgentSpendTable.failedRequests
    var totalResponseTimeMs by LiteLLMDailyAgentSpendTable.totalResponseTimeMs
    var timedRequests by LiteLLMDailyAgentSpendTable.timedRequests
    var createdAt by LiteLLMDailyAgentSpendTable.createdAt
    var updatedAt by LiteLLMDailyAgentSpendTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(LiteLLMDailyAgentSpendTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
