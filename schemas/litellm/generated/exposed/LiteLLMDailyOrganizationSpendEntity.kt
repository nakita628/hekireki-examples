package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class LiteLLMDailyOrganizationSpendEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LiteLLMDailyOrganizationSpendEntity>(LiteLLMDailyOrganizationSpendTable)

    var organizationId by LiteLLMDailyOrganizationSpendTable.organizationId
    var date by LiteLLMDailyOrganizationSpendTable.date
    var apiKey by LiteLLMDailyOrganizationSpendTable.apiKey
    var model by LiteLLMDailyOrganizationSpendTable.model
    var modelGroup by LiteLLMDailyOrganizationSpendTable.modelGroup
    var customLlmProvider by LiteLLMDailyOrganizationSpendTable.customLlmProvider
    var mcpNamespacedToolName by LiteLLMDailyOrganizationSpendTable.mcpNamespacedToolName
    var endpoint by LiteLLMDailyOrganizationSpendTable.endpoint
    var promptTokens by LiteLLMDailyOrganizationSpendTable.promptTokens
    var completionTokens by LiteLLMDailyOrganizationSpendTable.completionTokens
    var cacheReadInputTokens by LiteLLMDailyOrganizationSpendTable.cacheReadInputTokens
    var cacheCreationInputTokens by LiteLLMDailyOrganizationSpendTable.cacheCreationInputTokens
    var compressionSavedTokens by LiteLLMDailyOrganizationSpendTable.compressionSavedTokens
    var compressionSavingsSpend by LiteLLMDailyOrganizationSpendTable.compressionSavingsSpend
    var promptCachingSavingsSpend by LiteLLMDailyOrganizationSpendTable.promptCachingSavingsSpend
    var gatewayInjectedCachingSavingsSpend by LiteLLMDailyOrganizationSpendTable.gatewayInjectedCachingSavingsSpend
    var autorouterSavingsSpend by LiteLLMDailyOrganizationSpendTable.autorouterSavingsSpend
    var guardrailSpend by LiteLLMDailyOrganizationSpendTable.guardrailSpend
    var spend by LiteLLMDailyOrganizationSpendTable.spend
    var apiRequests by LiteLLMDailyOrganizationSpendTable.apiRequests
    var successfulRequests by LiteLLMDailyOrganizationSpendTable.successfulRequests
    var failedRequests by LiteLLMDailyOrganizationSpendTable.failedRequests
    var totalResponseTimeMs by LiteLLMDailyOrganizationSpendTable.totalResponseTimeMs
    var timedRequests by LiteLLMDailyOrganizationSpendTable.timedRequests
    var createdAt by LiteLLMDailyOrganizationSpendTable.createdAt
    var updatedAt by LiteLLMDailyOrganizationSpendTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(LiteLLMDailyOrganizationSpendTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
