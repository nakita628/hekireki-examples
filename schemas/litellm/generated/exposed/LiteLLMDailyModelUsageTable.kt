package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object LiteLLMDailyModelUsageTable : CompositeIdTable("\"LiteLLM_DailyModelUsage\"") {
    val date = text("date").entityId()
    val modelGroup = text("model_group").entityId()
    val model = text("model").entityId()
    val customLlmProvider = text("custom_llm_provider").entityId()
    val taskType = text("task_type").entityId()
    val spend = double("spend").default(0.0)
    val promptTokens = long("prompt_tokens").default(0L)
    val completionTokens = long("completion_tokens").default(0L)
    val requestCount = long("request_count").default(0L)
    val successfulRequests = long("successful_requests").default(0L)
    val failedRequests = long("failed_requests").default(0L)
    val totalResponseTimeMs = long("total_response_time_ms").default(0L)
    val timedRequests = long("timed_requests").default(0L)
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(date, modelGroup, model, customLlmProvider, taskType, name = "\"LiteLLM_DailyModelUsage_pkey\"")

    init {
        index("\"LiteLLM_DailyModelUsage_date_idx\"", false, date)
        index("\"LiteLLM_DailyModelUsage_model_group_idx\"", false, modelGroup)
    }
}
