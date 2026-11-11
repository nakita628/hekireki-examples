package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object LiteLLMManagedVectorStoresTableTable : IdTable<String>("\"LiteLLM_ManagedVectorStoresTable\"") {
    override val id = text("vector_store_id").entityId()
    val customLlmProvider = text("custom_llm_provider")
    val vectorStoreName = text("vector_store_name").nullable()
    val vectorStoreDescription = text("vector_store_description").nullable()
    val vectorStoreMetadata = jsonb<String>("vector_store_metadata", { it }, { it }).nullable()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val litellmCredentialName = text("litellm_credential_name").nullable()
    val litellmParams = jsonb<String>("litellm_params", { it }, { it }).nullable()
    val teamId = text("team_id").nullable()
    val userId = text("user_id").nullable()
    override val primaryKey = PrimaryKey(id, name = "\"LiteLLM_ManagedVectorStoresTable_pkey\"")

    init {
        index("\"LiteLLM_ManagedVectorStoresTable_team_id_idx\"", false, teamId)
        index("\"LiteLLM_ManagedVectorStoresTable_user_id_idx\"", false, userId)
    }
}
