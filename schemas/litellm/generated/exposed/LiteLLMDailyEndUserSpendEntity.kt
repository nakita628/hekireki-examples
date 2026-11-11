package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class LiteLLMDailyEndUserSpendEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LiteLLMDailyEndUserSpendEntity>(LiteLLMDailyEndUserSpendTable)

    var endUserId by LiteLLMDailyEndUserSpendTable.endUserId
    var date by LiteLLMDailyEndUserSpendTable.date
    var apiKey by LiteLLMDailyEndUserSpendTable.apiKey
    var model by LiteLLMDailyEndUserSpendTable.model
    var modelGroup by LiteLLMDailyEndUserSpendTable.modelGroup
    var customLlmProvider by LiteLLMDailyEndUserSpendTable.customLlmProvider
    var mcpNamespacedToolName by LiteLLMDailyEndUserSpendTable.mcpNamespacedToolName
    var endpoint by LiteLLMDailyEndUserSpendTable.endpoint
    var promptTokens by LiteLLMDailyEndUserSpendTable.promptTokens
    var completionTokens by LiteLLMDailyEndUserSpendTable.completionTokens
    var cacheReadInputTokens by LiteLLMDailyEndUserSpendTable.cacheReadInputTokens
    var cacheCreationInputTokens by LiteLLMDailyEndUserSpendTable.cacheCreationInputTokens
    var compressionSavedTokens by LiteLLMDailyEndUserSpendTable.compressionSavedTokens
    var compressionSavingsSpend by LiteLLMDailyEndUserSpendTable.compressionSavingsSpend
    var promptCachingSavingsSpend by LiteLLMDailyEndUserSpendTable.promptCachingSavingsSpend
    var gatewayInjectedCachingSavingsSpend by LiteLLMDailyEndUserSpendTable.gatewayInjectedCachingSavingsSpend
    var autorouterSavingsSpend by LiteLLMDailyEndUserSpendTable.autorouterSavingsSpend
    var guardrailSpend by LiteLLMDailyEndUserSpendTable.guardrailSpend
    var spend by LiteLLMDailyEndUserSpendTable.spend
    var apiRequests by LiteLLMDailyEndUserSpendTable.apiRequests
    var successfulRequests by LiteLLMDailyEndUserSpendTable.successfulRequests
    var failedRequests by LiteLLMDailyEndUserSpendTable.failedRequests
    var totalResponseTimeMs by LiteLLMDailyEndUserSpendTable.totalResponseTimeMs
    var timedRequests by LiteLLMDailyEndUserSpendTable.timedRequests
    var createdAt by LiteLLMDailyEndUserSpendTable.createdAt
    var updatedAt by LiteLLMDailyEndUserSpendTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(LiteLLMDailyEndUserSpendTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
