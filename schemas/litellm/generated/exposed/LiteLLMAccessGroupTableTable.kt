package litellm.models

import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object LiteLLMAccessGroupTableTable : IdTable<String>("\"LiteLLM_AccessGroupTable\"") {
    override val id = text("access_group_id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val accessGroupName = text("access_group_name").uniqueIndex("\"LiteLLM_AccessGroupTable_access_group_name_key\"")
    val description = text("description").nullable()
    val accessModelNames = array("access_model_names", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val accessMcpServerIds = array("access_mcp_server_ids", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val mcpToolPermissions = jsonb<String>("mcp_tool_permissions", { it }, { it }).nullable()
    val accessAgentIds = array("access_agent_ids", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val accessSkillNames = array("access_skill_names", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val assignedTeamIds = array("assigned_team_ids", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val assignedKeyIds = array("assigned_key_ids", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val createdBy = text("created_by").nullable()
    val updatedAt = pgTimestamp("updated_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedBy = text("updated_by").nullable()
    override val primaryKey = PrimaryKey(id, name = "\"LiteLLM_AccessGroupTable_pkey\"")
}
