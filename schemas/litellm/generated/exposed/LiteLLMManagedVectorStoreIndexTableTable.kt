package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object LiteLLMManagedVectorStoreIndexTableTable : IdTable<String>("\"LiteLLM_ManagedVectorStoreIndexTable\"") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val indexName = text("index_name").uniqueIndex("\"LiteLLM_ManagedVectorStoreIndexTable_index_name_key\"")
    val litellmParams = jsonb<String>("litellm_params", { it }, { it })
    val indexInfo = jsonb<String>("index_info", { it }, { it }).nullable()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val createdBy = text("created_by").nullable()
    val updatedAt = pgTimestamp("updated_at", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedBy = text("updated_by").nullable()
    override val primaryKey = PrimaryKey(id, name = "\"LiteLLM_ManagedVectorStoreIndexTable_pkey\"")
}
