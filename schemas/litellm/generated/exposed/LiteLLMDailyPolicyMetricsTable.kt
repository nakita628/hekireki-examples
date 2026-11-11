package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object LiteLLMDailyPolicyMetricsTable : CompositeIdTable("\"LiteLLM_DailyPolicyMetrics\"") {
    val policyId = text("policy_id").entityId()
    val date = text("date").entityId()
    val requestsEvaluated = long("requests_evaluated").default(0L)
    val passedCount = long("passed_count").default(0L)
    val blockedCount = long("blocked_count").default(0L)
    val flaggedCount = long("flagged_count").default(0L)
    val avgScore = double("avg_score").nullable()
    val avgLatencyMs = double("avg_latency_ms").nullable()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(policyId, date, name = "\"LiteLLM_DailyPolicyMetrics_pkey\"")

    init {
        index("\"LiteLLM_DailyPolicyMetrics_date_idx\"", false, date)
        index("\"LiteLLM_DailyPolicyMetrics_policy_id_idx\"", false, policyId)
    }
}
