package litellm.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object LiteLLMCustomerOrganizationTableTable : IdTable<String>("\"LiteLLM_CustomerOrganizationTable\"") {
    val spend = double("spend").default(0.0)
    val budgetTemplateIds = array("budget_template_ids", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val budgetId =
        optReference(
            "budget_id",
            LiteLLMBudgetTableTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"LiteLLM_CustomerOrganizationTable_budget_id_fkey\"",
        )
    override val id = text("customer_org_id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val alias = text("alias").nullable()
    val defaultBudgetTemplateId =
        optReference(
            "default_budget_template_id",
            LiteLLMBudgetTableTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "LiteLLM_CustomerOrganizationTable_default_budget_template__fkey",
        )
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val createdBy = text("created_by")
    val updatedAt = pgTimestamp("updated_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedBy = text("updated_by")
    override val primaryKey = PrimaryKey(id, name = "\"LiteLLM_CustomerOrganizationTable_pkey\"")

    init {
        index("\"LiteLLM_CustomerOrganizationTable_spend_idx\"", false, spend)
    }
}
