package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class LiteLLMAutoRouterBaselineComparisonEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LiteLLMAutoRouterBaselineComparisonEntity>(LiteLLMAutoRouterBaselineComparisonTable)

    var apiKey by LiteLLMAutoRouterBaselineComparisonTable.apiKey
    var sessionId by LiteLLMAutoRouterBaselineComparisonTable.sessionId
    var routerName by LiteLLMAutoRouterBaselineComparisonTable.routerName
    var initialEquivalent by LiteLLMAutoRouterBaselineComparisonTable.initialEquivalent
    var revision by LiteLLMAutoRouterBaselineComparisonTable.revision
    var publishedRevision by LiteLLMAutoRouterBaselineComparisonTable.publishedRevision
    var history by LiteLLMAutoRouterBaselineComparisonTable.history
    var attemptedAt by LiteLLMAutoRouterBaselineComparisonTable.attemptedAt
    var retired by LiteLLMAutoRouterBaselineComparisonTable.retired
    var updatedAt by LiteLLMAutoRouterBaselineComparisonTable.updatedAt
}
