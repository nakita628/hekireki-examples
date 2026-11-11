package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class LiteLLMEndUserTableEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LiteLLMEndUserTableEntity>(LiteLLMEndUserTableTable)

    var alias by LiteLLMEndUserTableTable.alias
    var spend by LiteLLMEndUserTableTable.spend
    var allowedModelRegion by LiteLLMEndUserTableTable.allowedModelRegion
    var defaultModel by LiteLLMEndUserTableTable.defaultModel
    var models by LiteLLMEndUserTableTable.models
    var budgetId by LiteLLMEndUserTableTable.budgetId
    var objectPermissionId by LiteLLMEndUserTableTable.objectPermissionId
    var litellmBudgetTable by LiteLLMBudgetTableEntity optionalReferencedOn LiteLLMEndUserTableTable.budgetId
    var objectPermission by LiteLLMObjectPermissionTableEntity optionalReferencedOn LiteLLMEndUserTableTable.objectPermissionId
    var blocked by LiteLLMEndUserTableTable.blocked
}
