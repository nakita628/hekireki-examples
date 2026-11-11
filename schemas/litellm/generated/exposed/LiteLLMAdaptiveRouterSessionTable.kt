package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object LiteLLMAdaptiveRouterSessionTable : CompositeIdTable("\"LiteLLM_AdaptiveRouterSession\"") {
    val sessionId = text("session_id").entityId()
    val routerName = text("router_name").entityId()
    val modelName = text("model_name").entityId()
    val classifiedType = text("classified_type")
    val misalignmentCount = integer("misalignment_count").default(0)
    val stagnationCount = integer("stagnation_count").default(0)
    val disengagementCount = integer("disengagement_count").default(0)
    val satisfactionCount = integer("satisfaction_count").default(0)
    val failureCount = integer("failure_count").default(0)
    val loopCount = integer("loop_count").default(0)
    val exhaustionCount = integer("exhaustion_count").default(0)
    val lastUserContent = text("last_user_content").nullable()
    val lastAssistantContent = text("last_assistant_content").nullable()
    val toolCallHistory = jsonb<String>("tool_call_history", { it }, { it }).default("[]")
    val pendingToolCalls = jsonb<String>("pending_tool_calls", { it }, { it }).default("{}")
    val turnCount = integer("turn_count").default(0)
    val lastProcessedTurn = integer("last_processed_turn").default(-1)
    val cleanCreditAwarded = bool("clean_credit_awarded").default(false)
    val terminalStatus = integer("terminal_status").nullable()
    val lastActivityAt =
        pgTimestamp("last_activity_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(sessionId, routerName, modelName, name = "\"LiteLLM_AdaptiveRouterSession_pkey\"")

    init {
        index("idx_adaptive_router_session_activity", false, lastActivityAt)
    }
}
