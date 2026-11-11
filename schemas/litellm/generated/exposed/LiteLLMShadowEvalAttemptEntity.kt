package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class LiteLLMShadowEvalAttemptEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LiteLLMShadowEvalAttemptEntity>(LiteLLMShadowEvalAttemptTable)

    var jobId by LiteLLMShadowEvalAttemptTable.jobId
    var requestId by LiteLLMShadowEvalAttemptTable.requestId
    var outcome by LiteLLMShadowEvalAttemptTable.outcome
    var routerName by LiteLLMShadowEvalAttemptTable.routerName
    var tier by LiteLLMShadowEvalAttemptTable.tier
    var realModel by LiteLLMShadowEvalAttemptTable.realModel
    var shadowModel by LiteLLMShadowEvalAttemptTable.shadowModel
    var confidence by LiteLLMShadowEvalAttemptTable.confidence
    var judgeCost by LiteLLMShadowEvalAttemptTable.judgeCost
    var shadowCost by LiteLLMShadowEvalAttemptTable.shadowCost
    var realCost by LiteLLMShadowEvalAttemptTable.realCost
    var realClassifierCost by LiteLLMShadowEvalAttemptTable.realClassifierCost
    var shadowClassifierCost by LiteLLMShadowEvalAttemptTable.shadowClassifierCost
    var realCacheHit by LiteLLMShadowEvalAttemptTable.realCacheHit
    var error by LiteLLMShadowEvalAttemptTable.error
    var createdAt by LiteLLMShadowEvalAttemptTable.createdAt
}
