package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object LiteLLMSpendLogsTable : IdTable<String>("\"LiteLLM_SpendLogs\"") {
    override val id = text("request_id").entityId()
    val callType = text("call_type")
    val apiKey = text("api_key").default("")
    val spend = double("spend").default(0.0)
    val totalTokens = integer("total_tokens").default(0)
    val promptTokens = integer("prompt_tokens").default(0)
    val completionTokens = integer("completion_tokens").default(0)
    val startTime = pgTimestamp("startTime", 3)
    val endTime = pgTimestamp("endTime", 3)
    val requestDurationMs = integer("request_duration_ms").nullable()
    val completionStartTime = pgTimestamp("completionStartTime", 3).nullable()
    val model = text("model").default("")
    val modelId = text("model_id").nullable().default("")
    val modelGroup = text("model_group").nullable().default("")
    val customLlmProvider = text("custom_llm_provider").nullable().default("")
    val apiBase = text("api_base").nullable().default("")
    val user = text("user").nullable().default("")
    val metadata = jsonb<String>("metadata", { it }, { it }).nullable().default("{}")
    val cacheHit = text("cache_hit").nullable().default("")
    val cacheKey = text("cache_key").nullable().default("")
    val requestTags = jsonb<String>("request_tags", { it }, { it }).nullable().default("[]")
    val teamId = text("team_id").nullable()
    val organizationId = text("organization_id").nullable()
    val endUser = text("end_user").nullable()
    val requesterIpAddress = text("requester_ip_address").nullable()
    val messages = jsonb<String>("messages", { it }, { it }).nullable().default("{}")
    val response = jsonb<String>("response", { it }, { it }).nullable().default("{}")
    val sessionId = text("session_id").nullable()
    val status = text("status").nullable()
    val mcpNamespacedToolName = text("mcp_namespaced_tool_name").nullable()
    val billingAgentId = text("billing_agent_id").nullable()
    val agentId = text("agent_id").nullable()
    val proxyServerRequest = jsonb<String>("proxy_server_request", { it }, { it }).nullable().default("{}")
    val litellmCallId = text("litellm_call_id").nullable()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"LiteLLM_SpendLogs_pkey\"")

    init {
        index("\"LiteLLM_SpendLogs_startTime_idx\"", false, startTime)
        index("\"LiteLLM_SpendLogs_startTime_request_id_idx\"", false, startTime, id)
        index("\"LiteLLM_SpendLogs_end_user_idx\"", false, endUser)
        index("\"LiteLLM_SpendLogs_session_id_idx\"", false, sessionId)
        index("\"LiteLLM_SpendLogs_litellm_call_id_idx\"", false, litellmCallId)
        index("\"LiteLLM_SpendLogs_api_key_startTime_idx\"", false, apiKey, startTime)
    }
}
