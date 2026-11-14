package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class LiteLLMDailyCustomerMemberSpendEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LiteLLMDailyCustomerMemberSpendEntity>(LiteLLMDailyCustomerMemberSpendTable)

    var date by LiteLLMDailyCustomerMemberSpendTable.date
    var customerOrgId by LiteLLMDailyCustomerMemberSpendTable.customerOrgId
    var customerTeamId by LiteLLMDailyCustomerMemberSpendTable.customerTeamId
    var customerMemberId by LiteLLMDailyCustomerMemberSpendTable.customerMemberId
    var model by LiteLLMDailyCustomerMemberSpendTable.model
    var modelGroup by LiteLLMDailyCustomerMemberSpendTable.modelGroup
    var customLlmProvider by LiteLLMDailyCustomerMemberSpendTable.customLlmProvider
    var mcpNamespacedToolName by LiteLLMDailyCustomerMemberSpendTable.mcpNamespacedToolName
    var endpoint by LiteLLMDailyCustomerMemberSpendTable.endpoint
    var apiKey by LiteLLMDailyCustomerMemberSpendTable.apiKey
    var spend by LiteLLMDailyCustomerMemberSpendTable.spend
    var promptTokens by LiteLLMDailyCustomerMemberSpendTable.promptTokens
    var completionTokens by LiteLLMDailyCustomerMemberSpendTable.completionTokens
    var cacheReadInputTokens by LiteLLMDailyCustomerMemberSpendTable.cacheReadInputTokens
    var cacheCreationInputTokens by LiteLLMDailyCustomerMemberSpendTable.cacheCreationInputTokens
    var compressionSavedTokens by LiteLLMDailyCustomerMemberSpendTable.compressionSavedTokens
    var compressionSavingsSpend by LiteLLMDailyCustomerMemberSpendTable.compressionSavingsSpend
    var promptCachingSavingsSpend by LiteLLMDailyCustomerMemberSpendTable.promptCachingSavingsSpend
    var gatewayInjectedCachingSavingsSpend by LiteLLMDailyCustomerMemberSpendTable.gatewayInjectedCachingSavingsSpend
    var autorouterSavingsSpend by LiteLLMDailyCustomerMemberSpendTable.autorouterSavingsSpend
    var guardrailSpend by LiteLLMDailyCustomerMemberSpendTable.guardrailSpend
    var apiRequests by LiteLLMDailyCustomerMemberSpendTable.apiRequests
    var successfulRequests by LiteLLMDailyCustomerMemberSpendTable.successfulRequests
    var failedRequests by LiteLLMDailyCustomerMemberSpendTable.failedRequests
    var totalResponseTimeMs by LiteLLMDailyCustomerMemberSpendTable.totalResponseTimeMs
    var timedRequests by LiteLLMDailyCustomerMemberSpendTable.timedRequests
    var createdAt by LiteLLMDailyCustomerMemberSpendTable.createdAt
    var updatedAt by LiteLLMDailyCustomerMemberSpendTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(LiteLLMDailyCustomerMemberSpendTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
