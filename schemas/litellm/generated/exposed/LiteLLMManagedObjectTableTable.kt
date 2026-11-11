package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object LiteLLMManagedObjectTableTable : IdTable<String>("\"LiteLLM_ManagedObjectTable\"") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val unifiedObjectId = text("unified_object_id").uniqueIndex("\"LiteLLM_ManagedObjectTable_unified_object_id_key\"")
    val modelObjectId = text("model_object_id").uniqueIndex("\"LiteLLM_ManagedObjectTable_model_object_id_key\"")
    val fileObject = jsonb<String>("file_object", { it }, { it })
    val filePurpose = text("file_purpose")
    val status = text("status").nullable()
    val batchProcessed = bool("batch_processed").default(false)
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val createdBy = text("created_by").nullable()
    val teamId = text("team_id").nullable()
    val orgId = text("org_id").nullable()
    val apiKey = text("api_key").nullable()
    val requestTags = jsonb<String>("request_tags", { it }, { it }).nullable().default("[]")
    val endUserId = text("end_user_id").nullable()
    val updatedAt = pgTimestamp("updated_at", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedBy = text("updated_by").nullable()
    override val primaryKey = PrimaryKey(id, name = "\"LiteLLM_ManagedObjectTable_pkey\"")

    init {
        index("\"LiteLLM_ManagedObjectTable_unified_object_id_idx\"", false, unifiedObjectId)
        index("\"LiteLLM_ManagedObjectTable_model_object_id_idx\"", false, modelObjectId)
        index(
            "\"LiteLLM_ManagedObjectTable_team_id_created_at_idx\"",
            false,
            functions = listOf(indexExpression("\"team_id\""), indexExpression("\"created_at\" DESC")),
        )
    }
}
