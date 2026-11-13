package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class LiteLLMPerformanceAlertStateEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, LiteLLMPerformanceAlertStateEntity>(LiteLLMPerformanceAlertStateTable)

    var enabled by LiteLLMPerformanceAlertStateTable.enabled
    var alertsUsed by LiteLLMPerformanceAlertStateTable.alertsUsed
    var owner by LiteLLMPerformanceAlertStateTable.owner
    var nextRunAt by LiteLLMPerformanceAlertStateTable.nextRunAt
    var day by LiteLLMPerformanceAlertStateTable.day
    var scope by LiteLLMPerformanceAlertStateTable.scope
    var resumeAfter by LiteLLMPerformanceAlertStateTable.resumeAfter
    var wrapped by LiteLLMPerformanceAlertStateTable.wrapped
    var checked by LiteLLMPerformanceAlertStateTable.checked
    var skipped by LiteLLMPerformanceAlertStateTable.skipped
    var startScope by LiteLLMPerformanceAlertStateTable.startScope
    var startAfter by LiteLLMPerformanceAlertStateTable.startAfter
    var completedAt by LiteLLMPerformanceAlertStateTable.completedAt
    var updatedAt by LiteLLMPerformanceAlertStateTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(LiteLLMPerformanceAlertStateTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
