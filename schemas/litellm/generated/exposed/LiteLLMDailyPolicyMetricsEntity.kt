package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import java.time.Instant
import java.time.temporal.ChronoUnit

class LiteLLMDailyPolicyMetricsEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<LiteLLMDailyPolicyMetricsEntity>(LiteLLMDailyPolicyMetricsTable)

    val policyId by LiteLLMDailyPolicyMetricsTable.policyId
    val date by LiteLLMDailyPolicyMetricsTable.date
    var requestsEvaluated by LiteLLMDailyPolicyMetricsTable.requestsEvaluated
    var passedCount by LiteLLMDailyPolicyMetricsTable.passedCount
    var blockedCount by LiteLLMDailyPolicyMetricsTable.blockedCount
    var flaggedCount by LiteLLMDailyPolicyMetricsTable.flaggedCount
    var avgScore by LiteLLMDailyPolicyMetricsTable.avgScore
    var avgLatencyMs by LiteLLMDailyPolicyMetricsTable.avgLatencyMs
    var createdAt by LiteLLMDailyPolicyMetricsTable.createdAt
    var updatedAt by LiteLLMDailyPolicyMetricsTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(LiteLLMDailyPolicyMetricsTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
