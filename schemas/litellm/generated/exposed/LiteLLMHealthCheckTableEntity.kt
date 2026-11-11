package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class LiteLLMHealthCheckTableEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LiteLLMHealthCheckTableEntity>(LiteLLMHealthCheckTableTable)

    var modelName by LiteLLMHealthCheckTableTable.modelName
    var modelId by LiteLLMHealthCheckTableTable.modelId
    var status by LiteLLMHealthCheckTableTable.status
    var healthyCount by LiteLLMHealthCheckTableTable.healthyCount
    var unhealthyCount by LiteLLMHealthCheckTableTable.unhealthyCount
    var errorMessage by LiteLLMHealthCheckTableTable.errorMessage
    var responseTimeMs by LiteLLMHealthCheckTableTable.responseTimeMs
    var details by LiteLLMHealthCheckTableTable.details
    var checkedBy by LiteLLMHealthCheckTableTable.checkedBy
    var checkedAt by LiteLLMHealthCheckTableTable.checkedAt
    var createdAt by LiteLLMHealthCheckTableTable.createdAt
    var updatedAt by LiteLLMHealthCheckTableTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(LiteLLMHealthCheckTableTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
