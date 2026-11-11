package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class LiteLLMSpendLogsEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LiteLLMSpendLogsEntity>(LiteLLMSpendLogsTable)

    var callType by LiteLLMSpendLogsTable.callType
    var apiKey by LiteLLMSpendLogsTable.apiKey
    var spend by LiteLLMSpendLogsTable.spend
    var totalTokens by LiteLLMSpendLogsTable.totalTokens
    var promptTokens by LiteLLMSpendLogsTable.promptTokens
    var completionTokens by LiteLLMSpendLogsTable.completionTokens
    var startTime by LiteLLMSpendLogsTable.startTime
    var endTime by LiteLLMSpendLogsTable.endTime
    var requestDurationMs by LiteLLMSpendLogsTable.requestDurationMs
    var completionStartTime by LiteLLMSpendLogsTable.completionStartTime
    var model by LiteLLMSpendLogsTable.model
    var modelId by LiteLLMSpendLogsTable.modelId
    var modelGroup by LiteLLMSpendLogsTable.modelGroup
    var customLlmProvider by LiteLLMSpendLogsTable.customLlmProvider
    var apiBase by LiteLLMSpendLogsTable.apiBase
    var user by LiteLLMSpendLogsTable.user
    var metadata by LiteLLMSpendLogsTable.metadata
    var cacheHit by LiteLLMSpendLogsTable.cacheHit
    var cacheKey by LiteLLMSpendLogsTable.cacheKey
    var requestTags by LiteLLMSpendLogsTable.requestTags
    var teamId by LiteLLMSpendLogsTable.teamId
    var organizationId by LiteLLMSpendLogsTable.organizationId
    var endUser by LiteLLMSpendLogsTable.endUser
    var requesterIpAddress by LiteLLMSpendLogsTable.requesterIpAddress
    var messages by LiteLLMSpendLogsTable.messages
    var response by LiteLLMSpendLogsTable.response
    var sessionId by LiteLLMSpendLogsTable.sessionId
    var status by LiteLLMSpendLogsTable.status
    var mcpNamespacedToolName by LiteLLMSpendLogsTable.mcpNamespacedToolName
    var billingAgentId by LiteLLMSpendLogsTable.billingAgentId
    var agentId by LiteLLMSpendLogsTable.agentId
    var proxyServerRequest by LiteLLMSpendLogsTable.proxyServerRequest
    var litellmCallId by LiteLLMSpendLogsTable.litellmCallId
    var createdAt by LiteLLMSpendLogsTable.createdAt
    var updatedAt by LiteLLMSpendLogsTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(LiteLLMSpendLogsTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
