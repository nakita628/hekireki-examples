package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class LiteLLMTeamTableEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LiteLLMTeamTableEntity>(LiteLLMTeamTableTable)

    var teamAlias by LiteLLMTeamTableTable.teamAlias
    var organizationId by LiteLLMTeamTableTable.organizationId
    var objectPermissionId by LiteLLMTeamTableTable.objectPermissionId
    var admins by LiteLLMTeamTableTable.admins
    var members by LiteLLMTeamTableTable.members
    var membersWithRoles by LiteLLMTeamTableTable.membersWithRoles
    var metadata by LiteLLMTeamTableTable.metadata
    var maxBudget by LiteLLMTeamTableTable.maxBudget
    var softBudget by LiteLLMTeamTableTable.softBudget
    var spend by LiteLLMTeamTableTable.spend
    var models by LiteLLMTeamTableTable.models
    var maxParallelRequests by LiteLLMTeamTableTable.maxParallelRequests
    var tpmLimit by LiteLLMTeamTableTable.tpmLimit
    var rpmLimit by LiteLLMTeamTableTable.rpmLimit
    var tpdLimit by LiteLLMTeamTableTable.tpdLimit
    var budgetDuration by LiteLLMTeamTableTable.budgetDuration
    var budgetResetAt by LiteLLMTeamTableTable.budgetResetAt
    var blocked by LiteLLMTeamTableTable.blocked
    var createdAt by LiteLLMTeamTableTable.createdAt
    var updatedAt by LiteLLMTeamTableTable.updatedAt
    var modelSpend by LiteLLMTeamTableTable.modelSpend
    var modelMaxBudget by LiteLLMTeamTableTable.modelMaxBudget
    var routerSettings by LiteLLMTeamTableTable.routerSettings
    var teamMemberPermissions by LiteLLMTeamTableTable.teamMemberPermissions
    var accessGroupIds by LiteLLMTeamTableTable.accessGroupIds
    var policies by LiteLLMTeamTableTable.policies
    var defaultTeamMemberModels by LiteLLMTeamTableTable.defaultTeamMemberModels
    var budgetLimits by LiteLLMTeamTableTable.budgetLimits
    var modelId by LiteLLMTeamTableTable.modelId
    var allowTeamGuardrailConfig by LiteLLMTeamTableTable.allowTeamGuardrailConfig
    var litellmOrganizationTable by LiteLLMOrganizationTableEntity optionalReferencedOn LiteLLMTeamTableTable.organizationId
    var litellmModelTable by LiteLLMModelTableEntity optionalReferencedOn LiteLLMTeamTableTable.modelId
    var objectPermission by LiteLLMObjectPermissionTableEntity optionalReferencedOn LiteLLMTeamTableTable.objectPermissionId
    val projects by LiteLLMProjectTableEntity optionalReferrersOn LiteLLMProjectTableTable.teamId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(LiteLLMTeamTableTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
