package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class LiteLLMMemoryTableEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LiteLLMMemoryTableEntity>(LiteLLMMemoryTableTable)

    var key by LiteLLMMemoryTableTable.key
    var value by LiteLLMMemoryTableTable.value
    var metadata by LiteLLMMemoryTableTable.metadata
    var userId by LiteLLMMemoryTableTable.userId
    var teamId by LiteLLMMemoryTableTable.teamId
    var createdAt by LiteLLMMemoryTableTable.createdAt
    var createdBy by LiteLLMMemoryTableTable.createdBy
    var updatedAt by LiteLLMMemoryTableTable.updatedAt
    var updatedBy by LiteLLMMemoryTableTable.updatedBy

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(LiteLLMMemoryTableTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
