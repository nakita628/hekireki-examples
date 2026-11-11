package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object LiteLLMMCPUserEnvVarsTable : IdTable<String>("\"LiteLLM_MCPUserEnvVars\"") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val userId = text("user_id")
    val serverId = text("server_id")
    val valuesB64 = text("values_b64")
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"LiteLLM_MCPUserEnvVars_pkey\"")

    init {
        index("\"LiteLLM_MCPUserEnvVars_user_id_idx\"", false, userId)
        index("\"LiteLLM_MCPUserEnvVars_server_id_idx\"", false, serverId)
        uniqueIndex("\"LiteLLM_MCPUserEnvVars_user_id_server_id_key\"", userId, serverId)
    }
}
