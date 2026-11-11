package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class LiteLLMProxyModelTableEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LiteLLMProxyModelTableEntity>(LiteLLMProxyModelTableTable)

    var modelName by LiteLLMProxyModelTableTable.modelName
    var litellmParams by LiteLLMProxyModelTableTable.litellmParams
    var modelInfo by LiteLLMProxyModelTableTable.modelInfo
    var blocked by LiteLLMProxyModelTableTable.blocked
    var createdAt by LiteLLMProxyModelTableTable.createdAt
    var createdBy by LiteLLMProxyModelTableTable.createdBy
    var updatedAt by LiteLLMProxyModelTableTable.updatedAt
    var updatedBy by LiteLLMProxyModelTableTable.updatedBy

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(LiteLLMProxyModelTableTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
