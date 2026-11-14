package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class LiteLLMCustomerTeamTableEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LiteLLMCustomerTeamTableEntity>(LiteLLMCustomerTeamTableTable)

    var spend by LiteLLMCustomerTeamTableTable.spend
    var alias by LiteLLMCustomerTeamTableTable.alias
    var parentOrgId by LiteLLMCustomerTeamTableTable.parentOrgId
    var parentOrg by LiteLLMCustomerOrganizationTableEntity referencedOn LiteLLMCustomerTeamTableTable.parentOrgId
    var budgetId by LiteLLMCustomerTeamTableTable.budgetId
    var budget by LiteLLMBudgetTableEntity optionalReferencedOn LiteLLMCustomerTeamTableTable.budgetId
    var budgetOverrideId by LiteLLMCustomerTeamTableTable.budgetOverrideId
    var budgetOverride by LiteLLMBudgetTableEntity optionalReferencedOn LiteLLMCustomerTeamTableTable.budgetOverrideId
    var budgetOverrideExpiresAt by LiteLLMCustomerTeamTableTable.budgetOverrideExpiresAt
    var createdAt by LiteLLMCustomerTeamTableTable.createdAt
    var createdBy by LiteLLMCustomerTeamTableTable.createdBy
    var updatedAt by LiteLLMCustomerTeamTableTable.updatedAt
    var updatedBy by LiteLLMCustomerTeamTableTable.updatedBy

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(LiteLLMCustomerTeamTableTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
