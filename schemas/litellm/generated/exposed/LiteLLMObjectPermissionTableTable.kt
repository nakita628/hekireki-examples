package litellm.models

import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.util.UUID

object LiteLLMObjectPermissionTableTable : IdTable<String>("\"LiteLLM_ObjectPermissionTable\"") {
    override val id = text("object_permission_id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val mcpServers = array("mcp_servers", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val mcpAccessGroups = array("mcp_access_groups", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val mcpToolPermissions = jsonb<String>("mcp_tool_permissions", { it }, { it }).nullable()
    val vectorStores = array("vector_stores", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val agents = array("agents", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val agentAccessGroups = array("agent_access_groups", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val models = array("models", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val blockedTools = array("blocked_tools", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val mcpToolsets = array("mcp_toolsets", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val searchTools = array("search_tools", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val mcpToolSearchEnabled = bool("mcp_tool_search_enabled").nullable()
    val skills = array("skills", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    override val primaryKey = PrimaryKey(id, name = "\"LiteLLM_ObjectPermissionTable_pkey\"")
}
