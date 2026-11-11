package litellm.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object LiteLLMProjectTableTable : IdTable<String>("\"LiteLLM_ProjectTable\"") {
    override val id = text("project_id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val projectAlias = text("project_alias").nullable()
    val description = text("description").nullable()
    val teamId =
        optReference(
            "team_id",
            LiteLLMTeamTableTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"LiteLLM_ProjectTable_team_id_fkey\"",
        )
    val budgetId =
        optReference(
            "budget_id",
            LiteLLMBudgetTableTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"LiteLLM_ProjectTable_budget_id_fkey\"",
        )
    val metadata = jsonb<String>("metadata", { it }, { it }).default("{}")
    val models = array("models", TextColumnType()).nullable()
    val spend = double("spend").default(0.0)
    val modelSpend = jsonb<String>("model_spend", { it }, { it }).default("{}")
    val modelRpmLimit = jsonb<String>("model_rpm_limit", { it }, { it }).default("{}")
    val modelTpmLimit = jsonb<String>("model_tpm_limit", { it }, { it }).default("{}")
    val blocked = bool("blocked").default(false)
    val objectPermissionId =
        optReference(
            "object_permission_id",
            LiteLLMObjectPermissionTableTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"LiteLLM_ProjectTable_object_permission_id_fkey\"",
        )
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val createdBy = text("created_by")
    val updatedAt = pgTimestamp("updated_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedBy = text("updated_by")
    override val primaryKey = PrimaryKey(id, name = "\"LiteLLM_ProjectTable_pkey\"")
}
