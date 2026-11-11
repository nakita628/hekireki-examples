package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object LiteLLMDailyGuardrailMetricsTable : CompositeIdTable("\"LiteLLM_DailyGuardrailMetrics\"") {
    val guardrailId = text("guardrail_id").entityId()
    val date = text("date").entityId()
    val requestsEvaluated = long("requests_evaluated").default(0L)
    val passedCount = long("passed_count").default(0L)
    val blockedCount = long("blocked_count").default(0L)
    val flaggedCount = long("flagged_count").default(0L)
    val avgScore = double("avg_score").nullable()
    val avgLatencyMs = double("avg_latency_ms").nullable()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(guardrailId, date, name = "\"LiteLLM_DailyGuardrailMetrics_pkey\"")

    init {
        index("\"LiteLLM_DailyGuardrailMetrics_date_idx\"", false, date)
        index("\"LiteLLM_DailyGuardrailMetrics_guardrail_id_idx\"", false, guardrailId)
    }
}
