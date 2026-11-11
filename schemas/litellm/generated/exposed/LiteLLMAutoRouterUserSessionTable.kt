package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable
import org.jetbrains.exposed.v1.json.jsonb

object LiteLLMAutoRouterUserSessionTable : CompositeIdTable("\"LiteLLM_AutoRouterUserSession\"") {
    val userId = text("user_id").entityId()
    val apiKey = text("api_key").entityId()
    val sessionId = text("session_id").entityId()
    val routerName = text("router_name").entityId()
    val routerType = text("router_type")
    val firstTurnAt = pgTimestamp("first_turn_at", 3)
    val lastTurnAt = pgTimestamp("last_turn_at", 3)
    val lastModel = text("last_model")
    val models = jsonb<String>("models", { it }, { it }).default("{}")
    val turns = integer("turns").default(0)
    val unorderedTurns = integer("unordered_turns").default(0)
    val coveredTurns = integer("covered_turns").default(0)
    val cacheHits = integer("cache_hits").default(0)
    val sameModelTurns = integer("same_model_turns").default(0)
    val sameModelHits = integer("same_model_hits").default(0)
    val firstVisitTurns = integer("first_visit_turns").default(0)
    val firstVisitHits = integer("first_visit_hits").default(0)
    val returnTurns = integer("return_turns").default(0)
    val returnHits = integer("return_hits").default(0)
    val returnExpiredMisses = integer("return_expired_misses").default(0)
    val returnWithinTtlMisses = integer("return_within_ttl_misses").default(0)
    val ttl5mTurns = integer("ttl_5m_turns").default(0)
    val ttl1hTurns = integer("ttl_1h_turns").default(0)
    val totalTokens = long("total_tokens").default(0L)
    val spend = double("spend").default(0.0)
    val savedSpend = double("saved_spend").default(0.0)
    val savingsEstimatedTurns = integer("savings_estimated_turns").default(0)
    val savingsEstimatedActualSpend = double("savings_estimated_actual_spend").default(0.0)
    val savingsEstimatedSavedSpend = double("savings_estimated_saved_spend").default(0.0)
    val savingsEstimatedBaselineModels = jsonb<String>("savings_estimated_baseline_models", { it }, { it }).default("{}")
    val classifierCost = double("classifier_cost").default(0.0)
    val classifierCostRecordedTurns = integer("classifier_cost_recorded_turns").default(0)
    val tierTurns = jsonb<String>("tier_turns", { it }, { it }).default("{}")
    val baselineModels = jsonb<String>("baseline_models", { it }, { it }).default("{}")
    override val primaryKey = PrimaryKey(userId, apiKey, sessionId, routerName, name = "\"LiteLLM_AutoRouterUserSession_pkey\"")

    init {
        index("idx_autorouter_user_session_last_turn", false, lastTurnAt)
        index("idx_autorouter_user_session_user_last_turn", false, userId, lastTurnAt)
    }
}
