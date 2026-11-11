package litellm.models

import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object LiteLLMManagedFileTableTable : IdTable<String>("\"LiteLLM_ManagedFileTable\"") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val unifiedFileId = text("unified_file_id").uniqueIndex("\"LiteLLM_ManagedFileTable_unified_file_id_key\"")
    val fileObject = jsonb<String>("file_object", { it }, { it }).nullable()
    val modelMappings = jsonb<String>("model_mappings", { it }, { it })
    val flatModelFileIds = array("flat_model_file_ids", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val storageBackend = text("storage_backend").nullable()
    val storageUrl = text("storage_url").nullable()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val createdBy = text("created_by").nullable()
    val teamId = text("team_id").nullable()
    val updatedAt = pgTimestamp("updated_at", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedBy = text("updated_by").nullable()
    override val primaryKey = PrimaryKey(id, name = "\"LiteLLM_ManagedFileTable_pkey\"")

    init {
        index("\"LiteLLM_ManagedFileTable_unified_file_id_idx\"", false, unifiedFileId)
        index("\"LiteLLM_ManagedFileTable_flat_model_file_ids_idx\"", false, flatModelFileIds, indexType = "gin")
        index(
            "\"LiteLLM_ManagedFileTable_team_id_created_at_idx\"",
            false,
            functions = listOf(indexExpression("\"team_id\""), indexExpression("\"created_at\" DESC")),
        )
        index(
            "\"LiteLLM_ManagedFileTable_created_by_created_at_idx\"",
            false,
            functions = listOf(indexExpression("\"created_by\""), indexExpression("\"created_at\" DESC")),
        )
    }
}
