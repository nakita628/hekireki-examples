package litellm.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable

object LiteLLMTeamMembershipTable : CompositeIdTable("\"LiteLLM_TeamMembership\"") {
    val userId = text("user_id").entityId()
    val teamId = text("team_id").entityId()
    val spend = double("spend").default(0.0)
    val totalSpend = double("total_spend").default(0.0)
    val budgetId =
        optReference(
            "budget_id",
            LiteLLMBudgetTableTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"LiteLLM_TeamMembership_budget_id_fkey\"",
        )
    override val primaryKey = PrimaryKey(userId, teamId, name = "\"LiteLLM_TeamMembership_pkey\"")
}
