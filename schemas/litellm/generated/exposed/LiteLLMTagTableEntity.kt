package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class LiteLLMTagTableEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LiteLLMTagTableEntity>(LiteLLMTagTableTable)

    var description by LiteLLMTagTableTable.description
    var models by LiteLLMTagTableTable.models
    var modelInfo by LiteLLMTagTableTable.modelInfo
    var spend by LiteLLMTagTableTable.spend
    var budgetId by LiteLLMTagTableTable.budgetId
    var litellmBudgetTable by LiteLLMBudgetTableEntity optionalReferencedOn LiteLLMTagTableTable.budgetId
    var createdAt by LiteLLMTagTableTable.createdAt
    var createdBy by LiteLLMTagTableTable.createdBy
    var updatedAt by LiteLLMTagTableTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(LiteLLMTagTableTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
