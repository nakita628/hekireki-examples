package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import java.time.Instant
import java.time.temporal.ChronoUnit

class LiteLLMOrganizationMembershipEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<LiteLLMOrganizationMembershipEntity>(LiteLLMOrganizationMembershipTable)

    val userId by LiteLLMOrganizationMembershipTable.userId
    val organizationId by LiteLLMOrganizationMembershipTable.organizationId
    var userRole by LiteLLMOrganizationMembershipTable.userRole
    var spend by LiteLLMOrganizationMembershipTable.spend
    var budgetId by LiteLLMOrganizationMembershipTable.budgetId
    var createdAt by LiteLLMOrganizationMembershipTable.createdAt
    var updatedAt by LiteLLMOrganizationMembershipTable.updatedAt
    val user by LiteLLMUserTableEntity referencedOn LiteLLMOrganizationMembershipTable.userId
    val organization by LiteLLMOrganizationTableEntity referencedOn LiteLLMOrganizationMembershipTable.organizationId
    var litellmBudgetTable by LiteLLMBudgetTableEntity optionalReferencedOn LiteLLMOrganizationMembershipTable.budgetId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(LiteLLMOrganizationMembershipTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
