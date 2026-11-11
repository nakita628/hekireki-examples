package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import java.time.Instant
import java.time.temporal.ChronoUnit

class LiteLLMBudgetWindowSpendEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<LiteLLMBudgetWindowSpendEntity>(LiteLLMBudgetWindowSpendTable)

    val entityType by LiteLLMBudgetWindowSpendTable.entityType
    val entityId by LiteLLMBudgetWindowSpendTable.entityId
    val windowDuration by LiteLLMBudgetWindowSpendTable.windowDuration
    var windowStart by LiteLLMBudgetWindowSpendTable.windowStart
    var spend by LiteLLMBudgetWindowSpendTable.spend
    var createdAt by LiteLLMBudgetWindowSpendTable.createdAt
    var updatedAt by LiteLLMBudgetWindowSpendTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(LiteLLMBudgetWindowSpendTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
