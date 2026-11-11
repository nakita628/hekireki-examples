package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass

class LiteLLMTeamMembershipEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<LiteLLMTeamMembershipEntity>(LiteLLMTeamMembershipTable)

    val userId by LiteLLMTeamMembershipTable.userId
    val teamId by LiteLLMTeamMembershipTable.teamId
    var spend by LiteLLMTeamMembershipTable.spend
    var totalSpend by LiteLLMTeamMembershipTable.totalSpend
    var budgetId by LiteLLMTeamMembershipTable.budgetId
    var litellmBudgetTable by LiteLLMBudgetTableEntity optionalReferencedOn LiteLLMTeamMembershipTable.budgetId
}
