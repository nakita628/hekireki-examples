package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class LiteLLMUserTableEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LiteLLMUserTableEntity>(LiteLLMUserTableTable)

    val verifiedSubjects by LiteLLMVerifiedSubjectEntity optionalReferrersOn LiteLLMVerifiedSubjectTable.userId
    var userAlias by LiteLLMUserTableTable.userAlias
    var teamId by LiteLLMUserTableTable.teamId
    var ssoUserId by LiteLLMUserTableTable.ssoUserId
    var organizationId by LiteLLMUserTableTable.organizationId
    var objectPermissionId by LiteLLMUserTableTable.objectPermissionId
    var password by LiteLLMUserTableTable.password
    var passwordResetRequired by LiteLLMUserTableTable.passwordResetRequired
    var lastBreachCheckAt by LiteLLMUserTableTable.lastBreachCheckAt
    var teams by LiteLLMUserTableTable.teams
    var userRole by LiteLLMUserTableTable.userRole
    var maxBudget by LiteLLMUserTableTable.maxBudget
    var spend by LiteLLMUserTableTable.spend
    var userEmail by LiteLLMUserTableTable.userEmail
    var models by LiteLLMUserTableTable.models
    var metadata by LiteLLMUserTableTable.metadata
    var maxParallelRequests by LiteLLMUserTableTable.maxParallelRequests
    var tpmLimit by LiteLLMUserTableTable.tpmLimit
    var rpmLimit by LiteLLMUserTableTable.rpmLimit
    var budgetDuration by LiteLLMUserTableTable.budgetDuration
    var budgetResetAt by LiteLLMUserTableTable.budgetResetAt
    var allowedCacheControls by LiteLLMUserTableTable.allowedCacheControls
    var policies by LiteLLMUserTableTable.policies
    var modelSpend by LiteLLMUserTableTable.modelSpend
    var modelMaxBudget by LiteLLMUserTableTable.modelMaxBudget
    var createdAt by LiteLLMUserTableTable.createdAt
    var updatedAt by LiteLLMUserTableTable.updatedAt
    var litellmOrganizationTable by LiteLLMOrganizationTableEntity optionalReferencedOn LiteLLMUserTableTable.organizationId
    val organizationMemberships by LiteLLMOrganizationMembershipEntity referrersOn LiteLLMOrganizationMembershipTable.userId
    val invitationsCreated by LiteLLMInvitationLinkEntity referrersOn LiteLLMInvitationLinkTable.createdBy
    val invitationsUpdated by LiteLLMInvitationLinkEntity referrersOn LiteLLMInvitationLinkTable.updatedBy
    val invitationsUser by LiteLLMInvitationLinkEntity referrersOn LiteLLMInvitationLinkTable.userId
    var objectPermission by LiteLLMObjectPermissionTableEntity optionalReferencedOn LiteLLMUserTableTable.objectPermissionId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(LiteLLMUserTableTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
