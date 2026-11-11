package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object LiteLLMDailyOrganizationSpendTable : IdTable<String>("\"LiteLLM_DailyOrganizationSpend\"") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val organizationId = text("organization_id").nullable()
    val date = text("date")
    val apiKey = text("api_key")
    val model = text("model").nullable()
    val modelGroup = text("model_group").nullable()
    val customLlmProvider = text("custom_llm_provider").nullable()
    val mcpNamespacedToolName = text("mcp_namespaced_tool_name").nullable()
    val endpoint = text("endpoint").nullable()
    val promptTokens = long("prompt_tokens").default(0L)
    val completionTokens = long("completion_tokens").default(0L)
    val cacheReadInputTokens = long("cache_read_input_tokens").default(0L)
    val cacheCreationInputTokens = long("cache_creation_input_tokens").default(0L)
    val compressionSavedTokens = long("compression_saved_tokens").default(0L)
    val compressionSavingsSpend = double("compression_savings_spend").default(0.0)
    val promptCachingSavingsSpend = double("prompt_caching_savings_spend").default(0.0)
    val gatewayInjectedCachingSavingsSpend = double("gateway_injected_caching_savings_spend").default(0.0)
    val autorouterSavingsSpend = double("autorouter_savings_spend").default(0.0)
    val guardrailSpend = double("guardrail_spend").default(0.0)
    val spend = double("spend").default(0.0)
    val apiRequests = long("api_requests").default(0L)
    val successfulRequests = long("successful_requests").default(0L)
    val failedRequests = long("failed_requests").default(0L)
    val totalResponseTimeMs = long("total_response_time_ms").default(0L)
    val timedRequests = long("timed_requests").default(0L)
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"LiteLLM_DailyOrganizationSpend_pkey\"")

    init {
        index("\"LiteLLM_DailyOrganizationSpend_date_idx\"", false, date)
        index("\"LiteLLM_DailyOrganizationSpend_organization_id_date_idx\"", false, organizationId, date)
        index("\"LiteLLM_DailyOrganizationSpend_api_key_idx\"", false, apiKey)
        index("\"LiteLLM_DailyOrganizationSpend_model_idx\"", false, model)
        index("\"LiteLLM_DailyOrganizationSpend_mcp_namespaced_tool_name_idx\"", false, mcpNamespacedToolName)
        index("\"LiteLLM_DailyOrganizationSpend_endpoint_idx\"", false, endpoint)
        uniqueIndex(
            "LiteLLM_DailyOrganizationSpend_organization_id_date_api_key_key",
            organizationId,
            date,
            apiKey,
            model,
            customLlmProvider,
            mcpNamespacedToolName,
            endpoint,
        )
    }
}
