package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass

class LiteLLMAutoRouterUserSessionEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<LiteLLMAutoRouterUserSessionEntity>(LiteLLMAutoRouterUserSessionTable)

    val userId by LiteLLMAutoRouterUserSessionTable.userId
    val apiKey by LiteLLMAutoRouterUserSessionTable.apiKey
    val sessionId by LiteLLMAutoRouterUserSessionTable.sessionId
    val routerName by LiteLLMAutoRouterUserSessionTable.routerName
    var routerType by LiteLLMAutoRouterUserSessionTable.routerType
    var firstTurnAt by LiteLLMAutoRouterUserSessionTable.firstTurnAt
    var lastTurnAt by LiteLLMAutoRouterUserSessionTable.lastTurnAt
    var lastModel by LiteLLMAutoRouterUserSessionTable.lastModel
    var models by LiteLLMAutoRouterUserSessionTable.models
    var turns by LiteLLMAutoRouterUserSessionTable.turns
    var unorderedTurns by LiteLLMAutoRouterUserSessionTable.unorderedTurns
    var coveredTurns by LiteLLMAutoRouterUserSessionTable.coveredTurns
    var cacheHits by LiteLLMAutoRouterUserSessionTable.cacheHits
    var sameModelTurns by LiteLLMAutoRouterUserSessionTable.sameModelTurns
    var sameModelHits by LiteLLMAutoRouterUserSessionTable.sameModelHits
    var firstVisitTurns by LiteLLMAutoRouterUserSessionTable.firstVisitTurns
    var firstVisitHits by LiteLLMAutoRouterUserSessionTable.firstVisitHits
    var returnTurns by LiteLLMAutoRouterUserSessionTable.returnTurns
    var returnHits by LiteLLMAutoRouterUserSessionTable.returnHits
    var returnExpiredMisses by LiteLLMAutoRouterUserSessionTable.returnExpiredMisses
    var returnWithinTtlMisses by LiteLLMAutoRouterUserSessionTable.returnWithinTtlMisses
    var ttl5mTurns by LiteLLMAutoRouterUserSessionTable.ttl5mTurns
    var ttl1hTurns by LiteLLMAutoRouterUserSessionTable.ttl1hTurns
    var totalTokens by LiteLLMAutoRouterUserSessionTable.totalTokens
    var spend by LiteLLMAutoRouterUserSessionTable.spend
    var savedSpend by LiteLLMAutoRouterUserSessionTable.savedSpend
    var savingsEstimatedTurns by LiteLLMAutoRouterUserSessionTable.savingsEstimatedTurns
    var savingsEstimatedActualSpend by LiteLLMAutoRouterUserSessionTable.savingsEstimatedActualSpend
    var savingsEstimatedSavedSpend by LiteLLMAutoRouterUserSessionTable.savingsEstimatedSavedSpend
    var savingsEstimatedBaselineModels by LiteLLMAutoRouterUserSessionTable.savingsEstimatedBaselineModels
    var classifierCost by LiteLLMAutoRouterUserSessionTable.classifierCost
    var classifierCostRecordedTurns by LiteLLMAutoRouterUserSessionTable.classifierCostRecordedTurns
    var tierTurns by LiteLLMAutoRouterUserSessionTable.tierTurns
    var baselineModels by LiteLLMAutoRouterUserSessionTable.baselineModels
}
