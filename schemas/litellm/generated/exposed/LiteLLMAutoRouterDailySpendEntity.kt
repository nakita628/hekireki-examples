package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass

class LiteLLMAutoRouterDailySpendEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<LiteLLMAutoRouterDailySpendEntity>(LiteLLMAutoRouterDailySpendTable)

    val date by LiteLLMAutoRouterDailySpendTable.date
    val apiKey by LiteLLMAutoRouterDailySpendTable.apiKey
    val userId by LiteLLMAutoRouterDailySpendTable.userId
    val routerName by LiteLLMAutoRouterDailySpendTable.routerName
    val routerType by LiteLLMAutoRouterDailySpendTable.routerType
    var turns by LiteLLMAutoRouterDailySpendTable.turns
    var totalTokens by LiteLLMAutoRouterDailySpendTable.totalTokens
    var tokenRecordedTurns by LiteLLMAutoRouterDailySpendTable.tokenRecordedTurns
    var spend by LiteLLMAutoRouterDailySpendTable.spend
    var savedSpend by LiteLLMAutoRouterDailySpendTable.savedSpend
    var savingsEstimatedTurns by LiteLLMAutoRouterDailySpendTable.savingsEstimatedTurns
    var savingsEstimatedActualSpend by LiteLLMAutoRouterDailySpendTable.savingsEstimatedActualSpend
    var savingsEstimatedSavedSpend by LiteLLMAutoRouterDailySpendTable.savingsEstimatedSavedSpend
    var classifierCost by LiteLLMAutoRouterDailySpendTable.classifierCost
    var classifierCostRecordedTurns by LiteLLMAutoRouterDailySpendTable.classifierCostRecordedTurns
}
