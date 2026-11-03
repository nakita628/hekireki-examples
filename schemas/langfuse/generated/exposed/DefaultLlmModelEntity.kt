package langfuse.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class DefaultLlmModelEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, DefaultLlmModelEntity>(DefaultLlmModelTable)

    var createdAt by DefaultLlmModelTable.createdAt
    var updatedAt by DefaultLlmModelTable.updatedAt
    var projectId by DefaultLlmModelTable.projectId
    var project by ProjectEntity referencedOn DefaultLlmModelTable.projectId
    var llmApiKeyId by DefaultLlmModelTable.llmApiKeyId
    var llmApiKey by LlmApiKeysEntity referencedOn DefaultLlmModelTable.llmApiKeyId
    var provider by DefaultLlmModelTable.provider
    var adapter by DefaultLlmModelTable.adapter
    var model by DefaultLlmModelTable.model
    var modelParams by DefaultLlmModelTable.modelParams

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(DefaultLlmModelTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
