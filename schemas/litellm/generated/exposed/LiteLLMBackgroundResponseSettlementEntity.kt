package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class LiteLLMBackgroundResponseSettlementEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LiteLLMBackgroundResponseSettlementEntity>(LiteLLMBackgroundResponseSettlementTable)

    var settledAt by LiteLLMBackgroundResponseSettlementTable.settledAt
}
