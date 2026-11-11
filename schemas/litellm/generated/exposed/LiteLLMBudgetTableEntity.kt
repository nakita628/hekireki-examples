package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class LiteLLMBudgetTableEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LiteLLMBudgetTableEntity>(LiteLLMBudgetTableTable)

    var maxBudget by LiteLLMBudgetTableTable.maxBudget
    var softBudget by LiteLLMBudgetTableTable.softBudget
    var maxParallelRequests by LiteLLMBudgetTableTable.maxParallelRequests
    var tpmLimit by LiteLLMBudgetTableTable.tpmLimit
    var rpmLimit by LiteLLMBudgetTableTable.rpmLimit
    var tpdLimit by LiteLLMBudgetTableTable.tpdLimit
    var modelMaxBudget by LiteLLMBudgetTableTable.modelMaxBudget
    var modelTpmLimit by LiteLLMBudgetTableTable.modelTpmLimit
    var modelRpmLimit by LiteLLMBudgetTableTable.modelRpmLimit
    var budgetDuration by LiteLLMBudgetTableTable.budgetDuration
    var budgetResetAt by LiteLLMBudgetTableTable.budgetResetAt
    var allowedModels by LiteLLMBudgetTableTable.allowedModels
    var tempBudgetIncrease by LiteLLMBudgetTableTable.tempBudgetIncrease
    var tempBudgetExpiry by LiteLLMBudgetTableTable.tempBudgetExpiry
    var createdAt by LiteLLMBudgetTableTable.createdAt
    var createdBy by LiteLLMBudgetTableTable.createdBy
    var updatedAt by LiteLLMBudgetTableTable.updatedAt
    var updatedBy by LiteLLMBudgetTableTable.updatedBy
    val organization by LiteLLMOrganizationTableEntity referrersOn LiteLLMOrganizationTableTable.budgetId
    val projects by LiteLLMProjectTableEntity optionalReferrersOn LiteLLMProjectTableTable.budgetId
    val keys by LiteLLMVerificationTokenEntity optionalReferrersOn LiteLLMVerificationTokenTable.budgetId
    val endUsers by LiteLLMEndUserTableEntity optionalReferrersOn LiteLLMEndUserTableTable.budgetId
    val tags by LiteLLMTagTableEntity optionalReferrersOn LiteLLMTagTableTable.budgetId
    val modelAccessGroups by LiteLLMModelAccessGroupBudgetTableEntity optionalReferrersOn LiteLLMModelAccessGroupBudgetTableTable.budgetId
    val teamMembership by LiteLLMTeamMembershipEntity optionalReferrersOn LiteLLMTeamMembershipTable.budgetId
    val organizationMembership by LiteLLMOrganizationMembershipEntity optionalReferrersOn LiteLLMOrganizationMembershipTable.budgetId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(LiteLLMBudgetTableTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
