package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object LiteLLMSearchToolsTableTable : IdTable<String>("\"LiteLLM_SearchToolsTable\"") {
    override val id = text("search_tool_id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val searchToolName = text("search_tool_name").uniqueIndex("\"LiteLLM_SearchToolsTable_search_tool_name_key\"")
    val litellmParams = jsonb<String>("litellm_params", { it }, { it })
    val searchToolInfo = jsonb<String>("search_tool_info", { it }, { it }).nullable()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"LiteLLM_SearchToolsTable_pkey\"")
}
