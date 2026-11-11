package litellm.models

import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object LiteLLMManagedVectorStoreTableTable : IdTable<String>("\"LiteLLM_ManagedVectorStoreTable\"") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val unifiedResourceId = text("unified_resource_id").uniqueIndex("\"LiteLLM_ManagedVectorStoreTable_unified_resource_id_key\"")
    val resourceObject = jsonb<String>("resource_object", { it }, { it }).nullable()
    val modelMappings = jsonb<String>("model_mappings", { it }, { it })
    val flatModelResourceIds = array("flat_model_resource_ids", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val storageBackend = text("storage_backend").nullable()
    val storageUrl = text("storage_url").nullable()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val createdBy = text("created_by").nullable()
    val teamId = text("team_id").nullable()
    val updatedAt = pgTimestamp("updated_at", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedBy = text("updated_by").nullable()
    override val primaryKey = PrimaryKey(id, name = "\"LiteLLM_ManagedVectorStoreTable_pkey\"")

    init {
        index("\"LiteLLM_ManagedVectorStoreTable_unified_resource_id_idx\"", false, unifiedResourceId)
        index(
            "\"LiteLLM_ManagedVectorStoreTable_team_id_created_at_idx\"",
            false,
            functions = listOf(indexExpression("\"team_id\""), indexExpression("\"created_at\" DESC")),
        )
    }
}
