package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class LiteLLMProjectTableEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LiteLLMProjectTableEntity>(LiteLLMProjectTableTable)

    var projectAlias by LiteLLMProjectTableTable.projectAlias
    var description by LiteLLMProjectTableTable.description
    var teamId by LiteLLMProjectTableTable.teamId
    var budgetId by LiteLLMProjectTableTable.budgetId
    var metadata by LiteLLMProjectTableTable.metadata
    var models by LiteLLMProjectTableTable.models
    var spend by LiteLLMProjectTableTable.spend
    var modelSpend by LiteLLMProjectTableTable.modelSpend
    var modelRpmLimit by LiteLLMProjectTableTable.modelRpmLimit
    var modelTpmLimit by LiteLLMProjectTableTable.modelTpmLimit
    var blocked by LiteLLMProjectTableTable.blocked
    var objectPermissionId by LiteLLMProjectTableTable.objectPermissionId
    var createdAt by LiteLLMProjectTableTable.createdAt
    var createdBy by LiteLLMProjectTableTable.createdBy
    var updatedAt by LiteLLMProjectTableTable.updatedAt
    var updatedBy by LiteLLMProjectTableTable.updatedBy
    var litellmTeamTable by LiteLLMTeamTableEntity optionalReferencedOn LiteLLMProjectTableTable.teamId
    var litellmBudgetTable by LiteLLMBudgetTableEntity optionalReferencedOn LiteLLMProjectTableTable.budgetId
    val keys by LiteLLMVerificationTokenEntity optionalReferrersOn LiteLLMVerificationTokenTable.projectId
    var objectPermission by LiteLLMObjectPermissionTableEntity optionalReferencedOn LiteLLMProjectTableTable.objectPermissionId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(LiteLLMProjectTableTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
