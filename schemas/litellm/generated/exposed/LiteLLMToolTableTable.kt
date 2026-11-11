package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object LiteLLMToolTableTable : IdTable<String>("\"LiteLLM_ToolTable\"") {
    override val id = text("tool_id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val toolName = text("tool_name").uniqueIndex("\"LiteLLM_ToolTable_tool_name_key\"")
    val origin = text("origin").nullable()
    val inputPolicy = text("input_policy").default("untrusted")
    val outputPolicy = text("output_policy").default("untrusted")
    val callCount = integer("call_count").default(0)
    val assignments = jsonb<String>("assignments", { it }, { it }).nullable().default("{}")
    val keyHash = text("key_hash").nullable()
    val teamId = text("team_id").nullable()
    val keyAlias = text("key_alias").nullable()
    val userAgent = text("user_agent").nullable()
    val lastUsedAt = pgTimestamp("last_used_at", 3).nullable()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val createdBy = text("created_by").nullable()
    val updatedAt = pgTimestamp("updated_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedBy = text("updated_by").nullable()
    override val primaryKey = PrimaryKey(id, name = "\"LiteLLM_ToolTable_pkey\"")

    init {
        index("\"LiteLLM_ToolTable_input_policy_idx\"", false, inputPolicy)
        index("\"LiteLLM_ToolTable_output_policy_idx\"", false, outputPolicy)
        index("\"LiteLLM_ToolTable_team_id_idx\"", false, teamId)
    }
}
