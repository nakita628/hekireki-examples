package litellm.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object LiteLLMOrganizationMembershipTable : CompositeIdTable("\"LiteLLM_OrganizationMembership\"") {
    val userId =
        reference(
            "user_id",
            LiteLLMUserTableTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"LiteLLM_OrganizationMembership_user_id_fkey\"",
        )
    val organizationId =
        reference(
            "organization_id",
            LiteLLMOrganizationTableTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"LiteLLM_OrganizationMembership_organization_id_fkey\"",
        )
    val userRole = text("user_role").nullable()
    val spend = double("spend").nullable().default(0.0)
    val budgetId =
        optReference(
            "budget_id",
            LiteLLMBudgetTableTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"LiteLLM_OrganizationMembership_budget_id_fkey\"",
        )
    val createdAt =
        pgTimestamp("created_at", 3).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt =
        pgTimestamp("updated_at", 3).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(userId, organizationId, name = "\"LiteLLM_OrganizationMembership_pkey\"")

    init {
        addIdColumn(userId)
        addIdColumn(organizationId)
        uniqueIndex("\"LiteLLM_OrganizationMembership_user_id_organization_id_key\"", userId, organizationId)
    }
}
