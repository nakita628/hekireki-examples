package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable

object LiteLLMAutoRouterDailySpendTable : CompositeIdTable("\"LiteLLM_AutoRouterDailySpend\"") {
    val date = text("date").entityId()
    val apiKey = text("api_key").entityId()
    val userId = text("user_id").entityId()
    val routerName = text("router_name").entityId()
    val routerType = text("router_type").entityId()
    val turns = integer("turns").default(0)
    val totalTokens = long("total_tokens").default(0L)
    val tokenRecordedTurns = integer("token_recorded_turns").default(0)
    val spend = double("spend").default(0.0)
    val savedSpend = double("saved_spend").default(0.0)
    val savingsEstimatedTurns = integer("savings_estimated_turns").default(0)
    val savingsEstimatedActualSpend = double("savings_estimated_actual_spend").default(0.0)
    val savingsEstimatedSavedSpend = double("savings_estimated_saved_spend").default(0.0)
    val classifierCost = double("classifier_cost").default(0.0)
    val classifierCostRecordedTurns = integer("classifier_cost_recorded_turns").default(0)
    override val primaryKey = PrimaryKey(date, apiKey, userId, routerName, routerType, name = "\"LiteLLM_AutoRouterDailySpend_pkey\"")
}
