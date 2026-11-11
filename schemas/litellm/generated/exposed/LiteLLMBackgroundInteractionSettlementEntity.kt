package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class LiteLLMBackgroundInteractionSettlementEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LiteLLMBackgroundInteractionSettlementEntity>(LiteLLMBackgroundInteractionSettlementTable)

    var customLlmProvider by LiteLLMBackgroundInteractionSettlementTable.customLlmProvider
    var createContext by LiteLLMBackgroundInteractionSettlementTable.createContext
    var createdAt by LiteLLMBackgroundInteractionSettlementTable.createdAt
    var claimedAt by LiteLLMBackgroundInteractionSettlementTable.claimedAt
    var claimedBy by LiteLLMBackgroundInteractionSettlementTable.claimedBy
    var settledAt by LiteLLMBackgroundInteractionSettlementTable.settledAt
    var outcome by LiteLLMBackgroundInteractionSettlementTable.outcome
}
