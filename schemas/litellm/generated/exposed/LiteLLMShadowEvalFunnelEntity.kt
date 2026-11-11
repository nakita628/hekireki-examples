package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class LiteLLMShadowEvalFunnelEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LiteLLMShadowEvalFunnelEntity>(LiteLLMShadowEvalFunnelTable)

    var notSampled by LiteLLMShadowEvalFunnelTable.notSampled
    var unjudgeable by LiteLLMShadowEvalFunnelTable.unjudgeable
    var shed by LiteLLMShadowEvalFunnelTable.shed
    var withheld by LiteLLMShadowEvalFunnelTable.withheld
}
