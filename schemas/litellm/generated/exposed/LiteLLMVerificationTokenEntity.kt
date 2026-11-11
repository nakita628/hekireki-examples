package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class LiteLLMVerificationTokenEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LiteLLMVerificationTokenEntity>(LiteLLMVerificationTokenTable)

    var keyName by LiteLLMVerificationTokenTable.keyName
    var keyAlias by LiteLLMVerificationTokenTable.keyAlias
    var softBudgetCooldown by LiteLLMVerificationTokenTable.softBudgetCooldown
    var spend by LiteLLMVerificationTokenTable.spend
    var totalSpend by LiteLLMVerificationTokenTable.totalSpend
    var expires by LiteLLMVerificationTokenTable.expires
    var models by LiteLLMVerificationTokenTable.models
    var aliases by LiteLLMVerificationTokenTable.aliases
    var config by LiteLLMVerificationTokenTable.config
    var routerSettings by LiteLLMVerificationTokenTable.routerSettings
    var userId by LiteLLMVerificationTokenTable.userId
    var teamId by LiteLLMVerificationTokenTable.teamId
    var agentId by LiteLLMVerificationTokenTable.agentId
    var projectId by LiteLLMVerificationTokenTable.projectId
    var permissions by LiteLLMVerificationTokenTable.permissions
    var maxParallelRequests by LiteLLMVerificationTokenTable.maxParallelRequests
    var metadata by LiteLLMVerificationTokenTable.metadata
    var blocked by LiteLLMVerificationTokenTable.blocked
    var tpmLimit by LiteLLMVerificationTokenTable.tpmLimit
    var rpmLimit by LiteLLMVerificationTokenTable.rpmLimit
    var tpdLimit by LiteLLMVerificationTokenTable.tpdLimit
    var maxBudget by LiteLLMVerificationTokenTable.maxBudget
    var budgetDuration by LiteLLMVerificationTokenTable.budgetDuration
    var budgetResetAt by LiteLLMVerificationTokenTable.budgetResetAt
    var allowedCacheControls by LiteLLMVerificationTokenTable.allowedCacheControls
    var allowedRoutes by LiteLLMVerificationTokenTable.allowedRoutes
    var keyType by LiteLLMVerificationTokenTable.keyType
    var policies by LiteLLMVerificationTokenTable.policies
    var accessGroupIds by LiteLLMVerificationTokenTable.accessGroupIds
    var modelSpend by LiteLLMVerificationTokenTable.modelSpend
    var modelMaxBudget by LiteLLMVerificationTokenTable.modelMaxBudget
    var budgetFallbacks by LiteLLMVerificationTokenTable.budgetFallbacks
    var budgetId by LiteLLMVerificationTokenTable.budgetId
    var organizationId by LiteLLMVerificationTokenTable.organizationId
    var objectPermissionId by LiteLLMVerificationTokenTable.objectPermissionId
    var createdAt by LiteLLMVerificationTokenTable.createdAt
    var createdBy by LiteLLMVerificationTokenTable.createdBy
    var updatedAt by LiteLLMVerificationTokenTable.updatedAt
    var updatedBy by LiteLLMVerificationTokenTable.updatedBy
    var settingsUpdatedAt by LiteLLMVerificationTokenTable.settingsUpdatedAt
    var lastActive by LiteLLMVerificationTokenTable.lastActive
    var rotationCount by LiteLLMVerificationTokenTable.rotationCount
    var autoRotate by LiteLLMVerificationTokenTable.autoRotate
    var rotationInterval by LiteLLMVerificationTokenTable.rotationInterval
    var lastRotationAt by LiteLLMVerificationTokenTable.lastRotationAt
    var keyRotationAt by LiteLLMVerificationTokenTable.keyRotationAt
    var budgetLimits by LiteLLMVerificationTokenTable.budgetLimits
    var litellmBudgetTable by LiteLLMBudgetTableEntity optionalReferencedOn LiteLLMVerificationTokenTable.budgetId
    var litellmOrganizationTable by LiteLLMOrganizationTableEntity optionalReferencedOn LiteLLMVerificationTokenTable.organizationId
    var litellmProjectTable by LiteLLMProjectTableEntity optionalReferencedOn LiteLLMVerificationTokenTable.projectId
    var objectPermission by LiteLLMObjectPermissionTableEntity optionalReferencedOn LiteLLMVerificationTokenTable.objectPermissionId
    val jwtKeyMappings by LiteLLMJWTKeyMappingEntity referrersOn LiteLLMJWTKeyMappingTable.token

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(LiteLLMVerificationTokenTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
