package langfuse.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class TopicConfigEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, TopicConfigEntity>(TopicConfigTable)

    var createdAt by TopicConfigTable.createdAt
    var updatedAt by TopicConfigTable.updatedAt
    var createdBy by TopicConfigTable.createdBy
    var createdByUser by UserEntity optionalReferencedOn TopicConfigTable.createdBy
    var updatedBy by TopicConfigTable.updatedBy
    var updatedByUser by UserEntity optionalReferencedOn TopicConfigTable.updatedBy
    var projectId by TopicConfigTable.projectId
    var project by ProjectEntity referencedOn TopicConfigTable.projectId
    var enabled by TopicConfigTable.enabled
    var blockedAt by TopicConfigTable.blockedAt
    var blockReason by TopicConfigTable.blockReason
    var blockMessage by TopicConfigTable.blockMessage
    var summaryLlmApiKeyId by TopicConfigTable.summaryLlmApiKeyId
    var summaryLlmApiKey by LlmApiKeysEntity optionalReferencedOn TopicConfigTable.summaryLlmApiKeyId
    var summaryModel by TopicConfigTable.summaryModel
    var embeddingLlmApiKeyId by TopicConfigTable.embeddingLlmApiKeyId
    var embeddingLlmApiKey by LlmApiKeysEntity optionalReferencedOn TopicConfigTable.embeddingLlmApiKeyId
    var embeddingModel by TopicConfigTable.embeddingModel
    var embeddingDimensions by TopicConfigTable.embeddingDimensions
    var clusteringLlmApiKeyId by TopicConfigTable.clusteringLlmApiKeyId
    var clusteringLlmApiKey by LlmApiKeysEntity optionalReferencedOn TopicConfigTable.clusteringLlmApiKeyId
    var clusteringModel by TopicConfigTable.clusteringModel

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(TopicConfigTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
