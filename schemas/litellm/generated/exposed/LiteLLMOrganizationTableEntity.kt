package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class LiteLLMOrganizationTableEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LiteLLMOrganizationTableEntity>(LiteLLMOrganizationTableTable)

    var organizationAlias by LiteLLMOrganizationTableTable.organizationAlias
    var budgetId by LiteLLMOrganizationTableTable.budgetId
    var metadata by LiteLLMOrganizationTableTable.metadata
    var models by LiteLLMOrganizationTableTable.models
    var spend by LiteLLMOrganizationTableTable.spend
    var modelSpend by LiteLLMOrganizationTableTable.modelSpend
    var objectPermissionId by LiteLLMOrganizationTableTable.objectPermissionId
    var createdAt by LiteLLMOrganizationTableTable.createdAt
    var createdBy by LiteLLMOrganizationTableTable.createdBy
    var updatedAt by LiteLLMOrganizationTableTable.updatedAt
    var updatedBy by LiteLLMOrganizationTableTable.updatedBy
    var litellmBudgetTable by LiteLLMBudgetTableEntity referencedOn LiteLLMOrganizationTableTable.budgetId
    val teams by LiteLLMTeamTableEntity optionalReferrersOn LiteLLMTeamTableTable.organizationId
    val users by LiteLLMUserTableEntity optionalReferrersOn LiteLLMUserTableTable.organizationId
    val keys by LiteLLMVerificationTokenEntity optionalReferrersOn LiteLLMVerificationTokenTable.organizationId
    val members by LiteLLMOrganizationMembershipEntity referrersOn LiteLLMOrganizationMembershipTable.organizationId
    var objectPermission by LiteLLMObjectPermissionTableEntity optionalReferencedOn LiteLLMOrganizationTableTable.objectPermissionId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(LiteLLMOrganizationTableTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
