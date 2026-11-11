package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object LiteLLMAuditLogTable : IdTable<String>("\"LiteLLM_AuditLog\"") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val updatedAt = pgTimestamp("updated_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val changedBy = text("changed_by").default("")
    val changedByApiKey = text("changed_by_api_key").default("")
    val action = text("action")
    val tableName1 = text("table_name")
    val objectId = text("object_id")
    val beforeValue = jsonb<String>("before_value", { it }, { it }).nullable()
    val updatedValues = jsonb<String>("updated_values", { it }, { it }).nullable()
    override val primaryKey = PrimaryKey(id, name = "\"LiteLLM_AuditLog_pkey\"")
}
