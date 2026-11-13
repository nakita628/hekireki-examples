package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class LiteLLMCustomerOrganizationTableEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LiteLLMCustomerOrganizationTableEntity>(LiteLLMCustomerOrganizationTableTable)

    var spend by LiteLLMCustomerOrganizationTableTable.spend
    var budgetResetAt by LiteLLMCustomerOrganizationTableTable.budgetResetAt
    var budgetIds by LiteLLMCustomerOrganizationTableTable.budgetIds
    var budgetNames by LiteLLMCustomerOrganizationTableTable.budgetNames
    var budgetId by LiteLLMCustomerOrganizationTableTable.budgetId
    var budget by LiteLLMBudgetTableEntity optionalReferencedOn LiteLLMCustomerOrganizationTableTable.budgetId
    var alias by LiteLLMCustomerOrganizationTableTable.alias
    var defaultBudgetId by LiteLLMCustomerOrganizationTableTable.defaultBudgetId
    var defaultBudget by LiteLLMBudgetTableEntity optionalReferencedOn LiteLLMCustomerOrganizationTableTable.defaultBudgetId
    val teams by LiteLLMCustomerTeamTableEntity referrersOn LiteLLMCustomerTeamTableTable.parentOrgId
    var createdAt by LiteLLMCustomerOrganizationTableTable.createdAt
    var createdBy by LiteLLMCustomerOrganizationTableTable.createdBy
    var updatedAt by LiteLLMCustomerOrganizationTableTable.updatedAt
    var updatedBy by LiteLLMCustomerOrganizationTableTable.updatedBy

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(LiteLLMCustomerOrganizationTableTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
