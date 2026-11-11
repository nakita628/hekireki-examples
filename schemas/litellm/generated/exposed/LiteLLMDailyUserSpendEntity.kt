package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class LiteLLMDailyUserSpendEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LiteLLMDailyUserSpendEntity>(LiteLLMDailyUserSpendTable)

    var userId by LiteLLMDailyUserSpendTable.userId
    var date by LiteLLMDailyUserSpendTable.date
    var apiKey by LiteLLMDailyUserSpendTable.apiKey
    var model by LiteLLMDailyUserSpendTable.model
    var modelGroup by LiteLLMDailyUserSpendTable.modelGroup
    var customLlmProvider by LiteLLMDailyUserSpendTable.customLlmProvider
    var mcpNamespacedToolName by LiteLLMDailyUserSpendTable.mcpNamespacedToolName
    var endpoint by LiteLLMDailyUserSpendTable.endpoint
    var promptTokens by LiteLLMDailyUserSpendTable.promptTokens
    var completionTokens by LiteLLMDailyUserSpendTable.completionTokens
    var cacheReadInputTokens by LiteLLMDailyUserSpendTable.cacheReadInputTokens
    var cacheCreationInputTokens by LiteLLMDailyUserSpendTable.cacheCreationInputTokens
    var compressionSavedTokens by LiteLLMDailyUserSpendTable.compressionSavedTokens
    var compressionSavingsSpend by LiteLLMDailyUserSpendTable.compressionSavingsSpend
    var promptCachingSavingsSpend by LiteLLMDailyUserSpendTable.promptCachingSavingsSpend
    var gatewayInjectedCachingSavingsSpend by LiteLLMDailyUserSpendTable.gatewayInjectedCachingSavingsSpend
    var autorouterSavingsSpend by LiteLLMDailyUserSpendTable.autorouterSavingsSpend
    var guardrailSpend by LiteLLMDailyUserSpendTable.guardrailSpend
    var spend by LiteLLMDailyUserSpendTable.spend
    var apiRequests by LiteLLMDailyUserSpendTable.apiRequests
    var successfulRequests by LiteLLMDailyUserSpendTable.successfulRequests
    var failedRequests by LiteLLMDailyUserSpendTable.failedRequests
    var totalResponseTimeMs by LiteLLMDailyUserSpendTable.totalResponseTimeMs
    var timedRequests by LiteLLMDailyUserSpendTable.timedRequests
    var createdAt by LiteLLMDailyUserSpendTable.createdAt
    var updatedAt by LiteLLMDailyUserSpendTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(LiteLLMDailyUserSpendTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
