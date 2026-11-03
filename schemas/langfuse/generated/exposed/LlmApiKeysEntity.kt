package langfuse.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class LlmApiKeysEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LlmApiKeysEntity>(LlmApiKeysTable)

    var createdAt by LlmApiKeysTable.createdAt
    var updatedAt by LlmApiKeysTable.updatedAt
    var provider by LlmApiKeysTable.provider
    var adapter by LlmApiKeysTable.adapter
    var displaySecretKey by LlmApiKeysTable.displaySecretKey
    var secretKey by LlmApiKeysTable.secretKey
    var baseURL by LlmApiKeysTable.baseURL
    var customModels by LlmApiKeysTable.customModels
    var withDefaultModels by LlmApiKeysTable.withDefaultModels
    var extraHeaders by LlmApiKeysTable.extraHeaders
    var extraHeaderKeys by LlmApiKeysTable.extraHeaderKeys
    var config by LlmApiKeysTable.config
    var projectId by LlmApiKeysTable.projectId
    var project by ProjectEntity optionalReferencedOn LlmApiKeysTable.projectId
    var organizationId by LlmApiKeysTable.organizationId
    var organization by OrganizationEntity optionalReferencedOn LlmApiKeysTable.organizationId
    val defaultLlmModel by DefaultLlmModelEntity referrersOn DefaultLlmModelTable.llmApiKeyId
    val topicSummaryConfigs by TopicConfigEntity optionalReferrersOn TopicConfigTable.summaryLlmApiKeyId
    val topicEmbeddingConfigs by TopicConfigEntity optionalReferrersOn TopicConfigTable.embeddingLlmApiKeyId
    val topicClusteringConfigs by TopicConfigEntity optionalReferrersOn TopicConfigTable.clusteringLlmApiKeyId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(LlmApiKeysTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
