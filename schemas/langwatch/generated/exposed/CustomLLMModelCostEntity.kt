package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class CustomLLMModelCostEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, CustomLLMModelCostEntity>(CustomLLMModelCostTable)

    var organizationId by CustomLLMModelCostTable.organizationId
    var scopeType by CustomLLMModelCostTable.scopeType
    var scopeId by CustomLLMModelCostTable.scopeId
    var projectId by CustomLLMModelCostTable.projectId
    var model by CustomLLMModelCostTable.model
    var regex by CustomLLMModelCostTable.regex
    var inputCostPerToken by CustomLLMModelCostTable.inputCostPerToken
    var outputCostPerToken by CustomLLMModelCostTable.outputCostPerToken
    var cacheReadCostPerToken by CustomLLMModelCostTable.cacheReadCostPerToken
    var cacheCreationCostPerToken by CustomLLMModelCostTable.cacheCreationCostPerToken
    var cacheCreation1hCostPerToken by CustomLLMModelCostTable.cacheCreation1hCostPerToken
    var createdAt by CustomLLMModelCostTable.createdAt
    var updatedAt by CustomLLMModelCostTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(CustomLLMModelCostTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
