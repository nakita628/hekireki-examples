package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object LiteLLMPromptTableTable : IdTable<String>("\"LiteLLM_PromptTable\"") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val promptId = text("prompt_id")
    val version = integer("version").default(1)
    val environment = text("environment").default("development")
    val createdBy = text("created_by").nullable()
    val litellmParams = jsonb<String>("litellm_params", { it }, { it })
    val promptInfo = jsonb<String>("prompt_info", { it }, { it }).nullable()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"LiteLLM_PromptTable_pkey\"")

    init {
        index("\"LiteLLM_PromptTable_prompt_id_environment_idx\"", false, promptId, environment)
        index("\"LiteLLM_PromptTable_prompt_id_idx\"", false, promptId)
        uniqueIndex("\"LiteLLM_PromptTable_prompt_id_version_environment_key\"", promptId, version, environment)
    }
}
