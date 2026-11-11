package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import java.time.Instant
import java.time.temporal.ChronoUnit

class LiteLLMDailyToolSpendEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<LiteLLMDailyToolSpendEntity>(LiteLLMDailyToolSpendTable)

    val date by LiteLLMDailyToolSpendTable.date
    val toolName by LiteLLMDailyToolSpendTable.toolName
    var spend by LiteLLMDailyToolSpendTable.spend
    var totalTokens by LiteLLMDailyToolSpendTable.totalTokens
    var requestCount by LiteLLMDailyToolSpendTable.requestCount
    var createdAt by LiteLLMDailyToolSpendTable.createdAt
    var updatedAt by LiteLLMDailyToolSpendTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(LiteLLMDailyToolSpendTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
