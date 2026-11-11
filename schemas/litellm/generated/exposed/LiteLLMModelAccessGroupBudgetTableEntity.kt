package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class LiteLLMModelAccessGroupBudgetTableEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LiteLLMModelAccessGroupBudgetTableEntity>(LiteLLMModelAccessGroupBudgetTableTable)

    var spend by LiteLLMModelAccessGroupBudgetTableTable.spend
    var budgetId by LiteLLMModelAccessGroupBudgetTableTable.budgetId
    var litellmBudgetTable by LiteLLMBudgetTableEntity optionalReferencedOn LiteLLMModelAccessGroupBudgetTableTable.budgetId
    var createdAt by LiteLLMModelAccessGroupBudgetTableTable.createdAt
    var createdBy by LiteLLMModelAccessGroupBudgetTableTable.createdBy
    var updatedAt by LiteLLMModelAccessGroupBudgetTableTable.updatedAt
    var updatedBy by LiteLLMModelAccessGroupBudgetTableTable.updatedBy

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(LiteLLMModelAccessGroupBudgetTableTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
