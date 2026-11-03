package langfuse.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object DefaultLlmModelTable : IdTable<String>("default_llm_models") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val projectId =
        reference(
            "project_id",
            ProjectTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "default_llm_models_project_id_fkey",
        ).uniqueIndex("default_llm_models_project_id_key")
    val llmApiKeyId =
        reference(
            "llm_api_key_id",
            LlmApiKeysTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "default_llm_models_llm_api_key_id_fkey",
        )
    val provider = text("provider")
    val adapter = text("adapter")
    val model = text("model")
    val modelParams = jsonb<String>("model_params", { it }, { it }).nullable()
    override val primaryKey = PrimaryKey(id, name = "default_llm_models_pkey")
}
