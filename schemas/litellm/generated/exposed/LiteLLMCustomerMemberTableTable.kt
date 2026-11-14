package litellm.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object LiteLLMCustomerMemberTableTable : IdTable<String>("\"LiteLLM_CustomerMemberTable\"") {
    val spend = double("spend").default(0.0)
    override val id = text("customer_member_id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val alias = text("alias").nullable()
    val parentOrgId =
        reference(
            "parent_org_id",
            LiteLLMCustomerOrganizationTableTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"LiteLLM_CustomerMemberTable_parent_org_id_fkey\"",
        )
    val parentTeamIds = array("parent_team_ids", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val budgetId =
        optReference(
            "budget_id",
            LiteLLMBudgetTableTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"LiteLLM_CustomerMemberTable_budget_id_fkey\"",
        )
    val budgetOverrideId =
        optReference(
            "budget_override_id",
            LiteLLMBudgetTableTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"LiteLLM_CustomerMemberTable_budget_override_id_fkey\"",
        )
    val budgetOverrideExpiresAt = pgTimestamp("budget_override_expires_at", 3).nullable()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val createdBy = text("created_by")
    val updatedAt = pgTimestamp("updated_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedBy = text("updated_by")
    override val primaryKey = PrimaryKey(id, name = "\"LiteLLM_CustomerMemberTable_pkey\"")

    init {
        index("\"LiteLLM_CustomerMemberTable_parent_org_id_idx\"", false, parentOrgId)
        index("\"LiteLLM_CustomerMemberTable_spend_idx\"", false, spend)
    }
}
