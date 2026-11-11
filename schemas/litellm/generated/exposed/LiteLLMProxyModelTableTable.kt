package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object LiteLLMProxyModelTableTable : IdTable<String>("\"LiteLLM_ProxyModelTable\"") {
    override val id = text("model_id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val modelName = text("model_name")
    val litellmParams = jsonb<String>("litellm_params", { it }, { it })
    val modelInfo = jsonb<String>("model_info", { it }, { it }).nullable()
    val blocked = bool("blocked").default(false)
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val createdBy = text("created_by")
    val updatedAt = pgTimestamp("updated_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedBy = text("updated_by")
    override val primaryKey = PrimaryKey(id, name = "\"LiteLLM_ProxyModelTable_pkey\"")
}
