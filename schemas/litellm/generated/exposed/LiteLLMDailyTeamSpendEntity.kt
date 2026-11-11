package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class LiteLLMDailyTeamSpendEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LiteLLMDailyTeamSpendEntity>(LiteLLMDailyTeamSpendTable)

    var teamId by LiteLLMDailyTeamSpendTable.teamId
    var date by LiteLLMDailyTeamSpendTable.date
    var apiKey by LiteLLMDailyTeamSpendTable.apiKey
    var model by LiteLLMDailyTeamSpendTable.model
    var modelGroup by LiteLLMDailyTeamSpendTable.modelGroup
    var customLlmProvider by LiteLLMDailyTeamSpendTable.customLlmProvider
    var mcpNamespacedToolName by LiteLLMDailyTeamSpendTable.mcpNamespacedToolName
    var endpoint by LiteLLMDailyTeamSpendTable.endpoint
    var promptTokens by LiteLLMDailyTeamSpendTable.promptTokens
    var completionTokens by LiteLLMDailyTeamSpendTable.completionTokens
    var cacheReadInputTokens by LiteLLMDailyTeamSpendTable.cacheReadInputTokens
    var cacheCreationInputTokens by LiteLLMDailyTeamSpendTable.cacheCreationInputTokens
    var compressionSavedTokens by LiteLLMDailyTeamSpendTable.compressionSavedTokens
    var compressionSavingsSpend by LiteLLMDailyTeamSpendTable.compressionSavingsSpend
    var promptCachingSavingsSpend by LiteLLMDailyTeamSpendTable.promptCachingSavingsSpend
    var gatewayInjectedCachingSavingsSpend by LiteLLMDailyTeamSpendTable.gatewayInjectedCachingSavingsSpend
    var autorouterSavingsSpend by LiteLLMDailyTeamSpendTable.autorouterSavingsSpend
    var guardrailSpend by LiteLLMDailyTeamSpendTable.guardrailSpend
    var spend by LiteLLMDailyTeamSpendTable.spend
    var apiRequests by LiteLLMDailyTeamSpendTable.apiRequests
    var successfulRequests by LiteLLMDailyTeamSpendTable.successfulRequests
    var failedRequests by LiteLLMDailyTeamSpendTable.failedRequests
    var totalResponseTimeMs by LiteLLMDailyTeamSpendTable.totalResponseTimeMs
    var timedRequests by LiteLLMDailyTeamSpendTable.timedRequests
    var ptuFlatCost by LiteLLMDailyTeamSpendTable.ptuFlatCost
    var createdAt by LiteLLMDailyTeamSpendTable.createdAt
    var updatedAt by LiteLLMDailyTeamSpendTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(LiteLLMDailyTeamSpendTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
