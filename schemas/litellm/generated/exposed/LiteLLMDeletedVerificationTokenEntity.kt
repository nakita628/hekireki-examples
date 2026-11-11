package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class LiteLLMDeletedVerificationTokenEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LiteLLMDeletedVerificationTokenEntity>(LiteLLMDeletedVerificationTokenTable)

    var token by LiteLLMDeletedVerificationTokenTable.token
    var keyName by LiteLLMDeletedVerificationTokenTable.keyName
    var keyAlias by LiteLLMDeletedVerificationTokenTable.keyAlias
    var softBudgetCooldown by LiteLLMDeletedVerificationTokenTable.softBudgetCooldown
    var spend by LiteLLMDeletedVerificationTokenTable.spend
    var totalSpend by LiteLLMDeletedVerificationTokenTable.totalSpend
    var expires by LiteLLMDeletedVerificationTokenTable.expires
    var models by LiteLLMDeletedVerificationTokenTable.models
    var aliases by LiteLLMDeletedVerificationTokenTable.aliases
    var config by LiteLLMDeletedVerificationTokenTable.config
    var userId by LiteLLMDeletedVerificationTokenTable.userId
    var teamId by LiteLLMDeletedVerificationTokenTable.teamId
    var agentId by LiteLLMDeletedVerificationTokenTable.agentId
    var projectId by LiteLLMDeletedVerificationTokenTable.projectId
    var permissions by LiteLLMDeletedVerificationTokenTable.permissions
    var maxParallelRequests by LiteLLMDeletedVerificationTokenTable.maxParallelRequests
    var metadata by LiteLLMDeletedVerificationTokenTable.metadata
    var blocked by LiteLLMDeletedVerificationTokenTable.blocked
    var tpmLimit by LiteLLMDeletedVerificationTokenTable.tpmLimit
    var rpmLimit by LiteLLMDeletedVerificationTokenTable.rpmLimit
    var tpdLimit by LiteLLMDeletedVerificationTokenTable.tpdLimit
    var maxBudget by LiteLLMDeletedVerificationTokenTable.maxBudget
    var budgetDuration by LiteLLMDeletedVerificationTokenTable.budgetDuration
    var budgetResetAt by LiteLLMDeletedVerificationTokenTable.budgetResetAt
    var allowedCacheControls by LiteLLMDeletedVerificationTokenTable.allowedCacheControls
    var allowedRoutes by LiteLLMDeletedVerificationTokenTable.allowedRoutes
    var keyType by LiteLLMDeletedVerificationTokenTable.keyType
    var policies by LiteLLMDeletedVerificationTokenTable.policies
    var accessGroupIds by LiteLLMDeletedVerificationTokenTable.accessGroupIds
    var modelSpend by LiteLLMDeletedVerificationTokenTable.modelSpend
    var modelMaxBudget by LiteLLMDeletedVerificationTokenTable.modelMaxBudget
    var budgetFallbacks by LiteLLMDeletedVerificationTokenTable.budgetFallbacks
    var routerSettings by LiteLLMDeletedVerificationTokenTable.routerSettings
    var budgetId by LiteLLMDeletedVerificationTokenTable.budgetId
    var organizationId by LiteLLMDeletedVerificationTokenTable.organizationId
    var objectPermissionId by LiteLLMDeletedVerificationTokenTable.objectPermissionId
    var createdAt by LiteLLMDeletedVerificationTokenTable.createdAt
    var createdBy by LiteLLMDeletedVerificationTokenTable.createdBy
    var updatedAt by LiteLLMDeletedVerificationTokenTable.updatedAt
    var updatedBy by LiteLLMDeletedVerificationTokenTable.updatedBy
    var settingsUpdatedAt by LiteLLMDeletedVerificationTokenTable.settingsUpdatedAt
    var lastActive by LiteLLMDeletedVerificationTokenTable.lastActive
    var rotationCount by LiteLLMDeletedVerificationTokenTable.rotationCount
    var autoRotate by LiteLLMDeletedVerificationTokenTable.autoRotate
    var rotationInterval by LiteLLMDeletedVerificationTokenTable.rotationInterval
    var lastRotationAt by LiteLLMDeletedVerificationTokenTable.lastRotationAt
    var keyRotationAt by LiteLLMDeletedVerificationTokenTable.keyRotationAt
    var deletedAt by LiteLLMDeletedVerificationTokenTable.deletedAt
    var deletedBy by LiteLLMDeletedVerificationTokenTable.deletedBy
    var deletedByApiKey by LiteLLMDeletedVerificationTokenTable.deletedByApiKey
    var litellmChangedBy by LiteLLMDeletedVerificationTokenTable.litellmChangedBy
}
