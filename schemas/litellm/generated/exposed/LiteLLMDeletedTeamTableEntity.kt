package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class LiteLLMDeletedTeamTableEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LiteLLMDeletedTeamTableEntity>(LiteLLMDeletedTeamTableTable)

    var teamId by LiteLLMDeletedTeamTableTable.teamId
    var teamAlias by LiteLLMDeletedTeamTableTable.teamAlias
    var organizationId by LiteLLMDeletedTeamTableTable.organizationId
    var objectPermissionId by LiteLLMDeletedTeamTableTable.objectPermissionId
    var admins by LiteLLMDeletedTeamTableTable.admins
    var members by LiteLLMDeletedTeamTableTable.members
    var membersWithRoles by LiteLLMDeletedTeamTableTable.membersWithRoles
    var metadata by LiteLLMDeletedTeamTableTable.metadata
    var maxBudget by LiteLLMDeletedTeamTableTable.maxBudget
    var softBudget by LiteLLMDeletedTeamTableTable.softBudget
    var spend by LiteLLMDeletedTeamTableTable.spend
    var models by LiteLLMDeletedTeamTableTable.models
    var maxParallelRequests by LiteLLMDeletedTeamTableTable.maxParallelRequests
    var tpmLimit by LiteLLMDeletedTeamTableTable.tpmLimit
    var rpmLimit by LiteLLMDeletedTeamTableTable.rpmLimit
    var tpdLimit by LiteLLMDeletedTeamTableTable.tpdLimit
    var budgetDuration by LiteLLMDeletedTeamTableTable.budgetDuration
    var budgetResetAt by LiteLLMDeletedTeamTableTable.budgetResetAt
    var blocked by LiteLLMDeletedTeamTableTable.blocked
    var modelSpend by LiteLLMDeletedTeamTableTable.modelSpend
    var modelMaxBudget by LiteLLMDeletedTeamTableTable.modelMaxBudget
    var routerSettings by LiteLLMDeletedTeamTableTable.routerSettings
    var teamMemberPermissions by LiteLLMDeletedTeamTableTable.teamMemberPermissions
    var accessGroupIds by LiteLLMDeletedTeamTableTable.accessGroupIds
    var policies by LiteLLMDeletedTeamTableTable.policies
    var modelId by LiteLLMDeletedTeamTableTable.modelId
    var allowTeamGuardrailConfig by LiteLLMDeletedTeamTableTable.allowTeamGuardrailConfig
    var createdAt by LiteLLMDeletedTeamTableTable.createdAt
    var updatedAt by LiteLLMDeletedTeamTableTable.updatedAt
    var deletedAt by LiteLLMDeletedTeamTableTable.deletedAt
    var deletedBy by LiteLLMDeletedTeamTableTable.deletedBy
    var deletedByApiKey by LiteLLMDeletedTeamTableTable.deletedByApiKey
    var litellmChangedBy by LiteLLMDeletedTeamTableTable.litellmChangedBy
}
