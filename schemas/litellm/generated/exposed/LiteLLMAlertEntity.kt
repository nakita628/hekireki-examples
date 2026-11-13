package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class LiteLLMAlertEntity(
    id: EntityID<Long>,
) : Entity<Long>(id) {
    companion object : EntityClass<Long, LiteLLMAlertEntity>(LiteLLMAlertTable)

    var detectionKey by LiteLLMAlertTable.detectionKey
    var type by LiteLLMAlertTable.type
    var resourceType by LiteLLMAlertTable.resourceType
    var resourceId by LiteLLMAlertTable.resourceId
    var status by LiteLLMAlertTable.status
    var windowStart by LiteLLMAlertTable.windowStart
    var windowEnd by LiteLLMAlertTable.windowEnd
    var evidence by LiteLLMAlertTable.evidence
    var createdAt by LiteLLMAlertTable.createdAt
    var updatedAt by LiteLLMAlertTable.updatedAt
    var updatedBy by LiteLLMAlertTable.updatedBy

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(LiteLLMAlertTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
