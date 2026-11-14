package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object LiteLLMDailyCustomerMemberSpendTable : IdTable<String>("\"LiteLLM_DailyCustomerMemberSpend\"") {
    override val id = text("id").entityId()
    val date = text("date")
    val customerOrgId = text("customer_org_id")
    val customerTeamId = text("customer_team_id").default("")
    val customerMemberId = text("customer_member_id").default("")
    val model = text("model").default("")
    val modelGroup = text("model_group").default("")
    val customLlmProvider = text("custom_llm_provider").default("")
    val mcpNamespacedToolName = text("mcp_namespaced_tool_name").default("")
    val endpoint = text("endpoint").default("")
    val apiKey = text("api_key").default("")
    val spend = double("spend").default(0.0)
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
    val apiRequests = long("api_requests").default(0L)
    val successfulRequests = long("successful_requests").default(0L)
    val failedRequests = long("failed_requests").default(0L)
    val totalResponseTimeMs = long("total_response_time_ms").default(0L)
    val timedRequests = long("timed_requests").default(0L)
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"LiteLLM_DailyCustomerMemberSpend_pkey\"")

    init {
        index("\"LiteLLM_DailyCustomerMemberSpend_date_idx\"", false, date)
        index("\"LiteLLM_DailyCustomerMemberSpend_customer_org_id_date_idx\"", false, customerOrgId, date)
        index("\"LiteLLM_DailyCustomerMemberSpend_customer_team_id_date_idx\"", false, customerTeamId, date)
        index("\"LiteLLM_DailyCustomerMemberSpend_customer_member_id_date_idx\"", false, customerMemberId, date)
        index("\"LiteLLM_DailyCustomerMemberSpend_api_key_idx\"", false, apiKey)
        index("\"LiteLLM_DailyCustomerMemberSpend_model_idx\"", false, model)
    }
}
