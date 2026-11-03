package langfuse.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object TopicConfigTable : IdTable<String>("topic_configs") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val createdBy =
        optReference(
            "created_by",
            UserTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "topic_configs_created_by_fkey",
        )
    val updatedBy =
        optReference(
            "updated_by",
            UserTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "topic_configs_updated_by_fkey",
        )
    val projectId =
        reference(
            "project_id",
            ProjectTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "topic_configs_project_id_fkey",
        ).uniqueIndex("topic_configs_project_id_key")
    val enabled = bool("enabled").default(false)
    val blockedAt = pgTimestamp("blocked_at", 3).nullable()
    val blockReason =
        pgEnum("block_reason", "\"EvaluatorBlockReason\"", EvaluatorBlockReason.entries, EvaluatorBlockReason::dbName).nullable()
    val blockMessage = text("block_message").nullable()
    val summaryLlmApiKeyId =
        optReference(
            "summary_llm_api_key_id",
            LlmApiKeysTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "topic_configs_summary_llm_api_key_id_fkey",
        )
    val summaryModel = text("summary_model").nullable()
    val embeddingLlmApiKeyId =
        optReference(
            "embedding_llm_api_key_id",
            LlmApiKeysTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "topic_configs_embedding_llm_api_key_id_fkey",
        )
    val embeddingModel = text("embedding_model").nullable()
    val embeddingDimensions = integer("embedding_dimensions").default(1024)
    val clusteringLlmApiKeyId =
        optReference(
            "clustering_llm_api_key_id",
            LlmApiKeysTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "topic_configs_clustering_llm_api_key_id_fkey",
        )
    val clusteringModel = text("clustering_model").nullable()
    override val primaryKey = PrimaryKey(id, name = "topic_configs_pkey")

    init {
        index("topic_configs_summary_llm_api_key_id_idx", false, summaryLlmApiKeyId)
        index("topic_configs_embedding_llm_api_key_id_idx", false, embeddingLlmApiKeyId)
        index("topic_configs_clustering_llm_api_key_id_idx", false, clusteringLlmApiKeyId)
    }
}
