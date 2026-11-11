package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object LiteLLMMCPToolsetTableTable : IdTable<String>("\"LiteLLM_MCPToolsetTable\"") {
    override val id = text("toolset_id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val toolsetName = text("toolset_name").uniqueIndex("\"LiteLLM_MCPToolsetTable_toolset_name_key\"")
    val description = text("description").nullable()
    val tools = jsonb<String>("tools", { it }, { it }).default("[]")
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val createdBy = text("created_by").nullable()
    val updatedAt = pgTimestamp("updated_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedBy = text("updated_by").nullable()
    override val primaryKey = PrimaryKey(id, name = "\"LiteLLM_MCPToolsetTable_pkey\"")
}
