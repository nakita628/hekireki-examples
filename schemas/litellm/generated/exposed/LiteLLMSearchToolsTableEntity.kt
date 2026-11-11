package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class LiteLLMSearchToolsTableEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LiteLLMSearchToolsTableEntity>(LiteLLMSearchToolsTableTable)

    var searchToolName by LiteLLMSearchToolsTableTable.searchToolName
    var litellmParams by LiteLLMSearchToolsTableTable.litellmParams
    var searchToolInfo by LiteLLMSearchToolsTableTable.searchToolInfo
    var createdAt by LiteLLMSearchToolsTableTable.createdAt
    var updatedAt by LiteLLMSearchToolsTableTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(LiteLLMSearchToolsTableTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
