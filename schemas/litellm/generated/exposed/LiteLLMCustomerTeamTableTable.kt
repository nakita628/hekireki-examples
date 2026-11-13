package litellm.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object LiteLLMCustomerTeamTableTable : IdTable<String>("\"LiteLLM_CustomerTeamTable\"") {
    val spend = double("spend").default(0.0)
    val budgetResetAt = pgTimestamp("budget_reset_at", 3).nullable()
    override val id = text("customer_team_id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val alias = text("alias").nullable()
    val parentOrgId =
        reference(
            "parent_org_id",
            LiteLLMCustomerOrganizationTableTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"LiteLLM_CustomerTeamTable_parent_org_id_fkey\"",
        )
    val budgetId =
        optReference(
            "budget_id",
            LiteLLMBudgetTableTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"LiteLLM_CustomerTeamTable_budget_id_fkey\"",
        )
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val createdBy = text("created_by")
    val updatedAt = pgTimestamp("updated_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedBy = text("updated_by")
    override val primaryKey = PrimaryKey(id, name = "\"LiteLLM_CustomerTeamTable_pkey\"")

    init {
        index("\"LiteLLM_CustomerTeamTable_parent_org_id_idx\"", false, parentOrgId)
    }
}
