package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class LiteLLMCustomerBudgetIncreaseRequestEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LiteLLMCustomerBudgetIncreaseRequestEntity>(LiteLLMCustomerBudgetIncreaseRequestTable)

    var customerMemberId by LiteLLMCustomerBudgetIncreaseRequestTable.customerMemberId
    var customerMember by LiteLLMCustomerMemberTableEntity referencedOn LiteLLMCustomerBudgetIncreaseRequestTable.customerMemberId
    var requestedMaxBudget by LiteLLMCustomerBudgetIncreaseRequestTable.requestedMaxBudget
    var expiresAt by LiteLLMCustomerBudgetIncreaseRequestTable.expiresAt
    var reason by LiteLLMCustomerBudgetIncreaseRequestTable.reason
    var status by LiteLLMCustomerBudgetIncreaseRequestTable.status
    var submittedBy by LiteLLMCustomerBudgetIncreaseRequestTable.submittedBy
    var submittedAt by LiteLLMCustomerBudgetIncreaseRequestTable.submittedAt
    var reviewedBy by LiteLLMCustomerBudgetIncreaseRequestTable.reviewedBy
    var reviewedAt by LiteLLMCustomerBudgetIncreaseRequestTable.reviewedAt
    var reviewNotes by LiteLLMCustomerBudgetIncreaseRequestTable.reviewNotes
}
