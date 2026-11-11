package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass

class LiteLLMAutoRouterSessionEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<LiteLLMAutoRouterSessionEntity>(LiteLLMAutoRouterSessionTable)

    val apiKey by LiteLLMAutoRouterSessionTable.apiKey
    val sessionId by LiteLLMAutoRouterSessionTable.sessionId
    val routerName by LiteLLMAutoRouterSessionTable.routerName
    var routerType by LiteLLMAutoRouterSessionTable.routerType
    var firstTurnAt by LiteLLMAutoRouterSessionTable.firstTurnAt
    var lastTurnAt by LiteLLMAutoRouterSessionTable.lastTurnAt
    var lastModel by LiteLLMAutoRouterSessionTable.lastModel
    var models by LiteLLMAutoRouterSessionTable.models
    var turns by LiteLLMAutoRouterSessionTable.turns
    var unorderedTurns by LiteLLMAutoRouterSessionTable.unorderedTurns
    var coveredTurns by LiteLLMAutoRouterSessionTable.coveredTurns
    var cacheHits by LiteLLMAutoRouterSessionTable.cacheHits
    var sameModelTurns by LiteLLMAutoRouterSessionTable.sameModelTurns
    var sameModelHits by LiteLLMAutoRouterSessionTable.sameModelHits
    var firstVisitTurns by LiteLLMAutoRouterSessionTable.firstVisitTurns
    var firstVisitHits by LiteLLMAutoRouterSessionTable.firstVisitHits
    var returnTurns by LiteLLMAutoRouterSessionTable.returnTurns
    var returnHits by LiteLLMAutoRouterSessionTable.returnHits
    var returnExpiredMisses by LiteLLMAutoRouterSessionTable.returnExpiredMisses
    var returnWithinTtlMisses by LiteLLMAutoRouterSessionTable.returnWithinTtlMisses
    var ttl5mTurns by LiteLLMAutoRouterSessionTable.ttl5mTurns
    var ttl1hTurns by LiteLLMAutoRouterSessionTable.ttl1hTurns
    var totalTokens by LiteLLMAutoRouterSessionTable.totalTokens
    var spend by LiteLLMAutoRouterSessionTable.spend
    var savedSpend by LiteLLMAutoRouterSessionTable.savedSpend
    var savingsEstimatedTurns by LiteLLMAutoRouterSessionTable.savingsEstimatedTurns
    var savingsEstimatedActualSpend by LiteLLMAutoRouterSessionTable.savingsEstimatedActualSpend
    var savingsEstimatedSavedSpend by LiteLLMAutoRouterSessionTable.savingsEstimatedSavedSpend
    var savingsEstimatedBaselineModels by LiteLLMAutoRouterSessionTable.savingsEstimatedBaselineModels
    var classifierCost by LiteLLMAutoRouterSessionTable.classifierCost
    var classifierCostRecordedTurns by LiteLLMAutoRouterSessionTable.classifierCostRecordedTurns
    var tierTurns by LiteLLMAutoRouterSessionTable.tierTurns
    var baselineModels by LiteLLMAutoRouterSessionTable.baselineModels
}
