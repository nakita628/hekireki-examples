package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class LiteLLMPromptTableEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LiteLLMPromptTableEntity>(LiteLLMPromptTableTable)

    var promptId by LiteLLMPromptTableTable.promptId
    var version by LiteLLMPromptTableTable.version
    var environment by LiteLLMPromptTableTable.environment
    var createdBy by LiteLLMPromptTableTable.createdBy
    var litellmParams by LiteLLMPromptTableTable.litellmParams
    var promptInfo by LiteLLMPromptTableTable.promptInfo
    var createdAt by LiteLLMPromptTableTable.createdAt
    var updatedAt by LiteLLMPromptTableTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(LiteLLMPromptTableTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
