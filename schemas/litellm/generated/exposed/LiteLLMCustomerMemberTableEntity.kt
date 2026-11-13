package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class LiteLLMCustomerMemberTableEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LiteLLMCustomerMemberTableEntity>(LiteLLMCustomerMemberTableTable)

    var spend by LiteLLMCustomerMemberTableTable.spend
    var budgetResetAt by LiteLLMCustomerMemberTableTable.budgetResetAt
    var alias by LiteLLMCustomerMemberTableTable.alias
    var parentTeamId by LiteLLMCustomerMemberTableTable.parentTeamId
    var parentTeam by LiteLLMCustomerTeamTableEntity referencedOn LiteLLMCustomerMemberTableTable.parentTeamId
    var budgetId by LiteLLMCustomerMemberTableTable.budgetId
    var budget by LiteLLMBudgetTableEntity optionalReferencedOn LiteLLMCustomerMemberTableTable.budgetId
    var budgetTemplateId by LiteLLMCustomerMemberTableTable.budgetTemplateId
    var budgetTemplate by LiteLLMBudgetTableEntity optionalReferencedOn LiteLLMCustomerMemberTableTable.budgetTemplateId
    var budgetOverrideExpiresAt by LiteLLMCustomerMemberTableTable.budgetOverrideExpiresAt
    var createdAt by LiteLLMCustomerMemberTableTable.createdAt
    var createdBy by LiteLLMCustomerMemberTableTable.createdBy
    var updatedAt by LiteLLMCustomerMemberTableTable.updatedAt
    var updatedBy by LiteLLMCustomerMemberTableTable.updatedBy

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(LiteLLMCustomerMemberTableTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
