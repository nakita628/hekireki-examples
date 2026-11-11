package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object LiteLLMBackgroundInteractionSettlementTable : IdTable<String>("\"LiteLLM_BackgroundInteractionSettlement\"") {
    override val id = text("interaction_id").entityId()
    val customLlmProvider = text("custom_llm_provider")
    val createContext = jsonb<String>("create_context", { it }, { it })
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val claimedAt = pgTimestamp("claimed_at", 3).nullable()
    val claimedBy = text("claimed_by").nullable()
    val settledAt = pgTimestamp("settled_at", 3).nullable()
    val outcome = text("outcome").nullable()
    override val primaryKey = PrimaryKey(id, name = "\"LiteLLM_BackgroundInteractionSettlement_pkey\"")

    init {
        index("idx_background_interaction_settlement_claimed_at", false, claimedAt)
        index("idx_background_interaction_settlement_settled_at", false, settledAt)
    }
}
