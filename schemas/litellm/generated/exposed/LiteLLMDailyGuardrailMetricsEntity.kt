package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import java.time.Instant
import java.time.temporal.ChronoUnit

class LiteLLMDailyGuardrailMetricsEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<LiteLLMDailyGuardrailMetricsEntity>(LiteLLMDailyGuardrailMetricsTable)

    val guardrailId by LiteLLMDailyGuardrailMetricsTable.guardrailId
    val date by LiteLLMDailyGuardrailMetricsTable.date
    var requestsEvaluated by LiteLLMDailyGuardrailMetricsTable.requestsEvaluated
    var passedCount by LiteLLMDailyGuardrailMetricsTable.passedCount
    var blockedCount by LiteLLMDailyGuardrailMetricsTable.blockedCount
    var flaggedCount by LiteLLMDailyGuardrailMetricsTable.flaggedCount
    var avgScore by LiteLLMDailyGuardrailMetricsTable.avgScore
    var avgLatencyMs by LiteLLMDailyGuardrailMetricsTable.avgLatencyMs
    var createdAt by LiteLLMDailyGuardrailMetricsTable.createdAt
    var updatedAt by LiteLLMDailyGuardrailMetricsTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(LiteLLMDailyGuardrailMetricsTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
