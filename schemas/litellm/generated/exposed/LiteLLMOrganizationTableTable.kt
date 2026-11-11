package litellm.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object LiteLLMOrganizationTableTable : IdTable<String>("\"LiteLLM_OrganizationTable\"") {
    override val id = text("organization_id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val organizationAlias = text("organization_alias")
    val budgetId =
        reference(
            "budget_id",
            LiteLLMBudgetTableTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"LiteLLM_OrganizationTable_budget_id_fkey\"",
        )
    val metadata = jsonb<String>("metadata", { it }, { it }).default("{}")
    val models = array("models", TextColumnType()).nullable()
    val spend = double("spend").default(0.0)
    val modelSpend = jsonb<String>("model_spend", { it }, { it }).default("{}")
    val objectPermissionId =
        optReference(
            "object_permission_id",
            LiteLLMObjectPermissionTableTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"LiteLLM_OrganizationTable_object_permission_id_fkey\"",
        )
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val createdBy = text("created_by")
    val updatedAt = pgTimestamp("updated_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedBy = text("updated_by")
    override val primaryKey = PrimaryKey(id, name = "\"LiteLLM_OrganizationTable_pkey\"")
}
