package litellm.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable

object LiteLLMEndUserTableTable : IdTable<String>("\"LiteLLM_EndUserTable\"") {
    override val id = text("user_id").entityId()
    val alias = text("alias").nullable()
    val spend = double("spend").default(0.0)
    val allowedModelRegion = text("allowed_model_region").nullable()
    val defaultModel = text("default_model").nullable()
    val models = array("models", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val budgetId =
        optReference(
            "budget_id",
            LiteLLMBudgetTableTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"LiteLLM_EndUserTable_budget_id_fkey\"",
        )
    val objectPermissionId =
        optReference(
            "object_permission_id",
            LiteLLMObjectPermissionTableTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"LiteLLM_EndUserTable_object_permission_id_fkey\"",
        )
    val blocked = bool("blocked").default(false)
    override val primaryKey = PrimaryKey(id, name = "\"LiteLLM_EndUserTable_pkey\"")
}
