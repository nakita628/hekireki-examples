package litellm.models

import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object LiteLLMMCPServerTableTable : IdTable<String>("\"LiteLLM_MCPServerTable\"") {
    override val id = text("server_id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val serverName = text("server_name").nullable()
    val alias = text("alias").nullable()
    val description = text("description").nullable()
    val instructions = text("instructions").nullable()
    val url = text("url").nullable()
    val specPath = text("spec_path").nullable()
    val transport = text("transport").default("sse")
    val authType = text("auth_type").nullable()
    val credentials = jsonb<String>("credentials", { it }, { it }).nullable().default("{}")
    val createdAt =
        pgTimestamp("created_at", 3).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val createdBy = text("created_by").nullable()
    val updatedAt =
        pgTimestamp("updated_at", 3).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedBy = text("updated_by").nullable()
    val mcpInfo = jsonb<String>("mcp_info", { it }, { it }).nullable().default("{}")
    val mcpAccessGroups = array("mcp_access_groups", TextColumnType()).nullable()
    val allowedTools = array("allowed_tools", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val toolNameToDisplayName = jsonb<String>("tool_name_to_display_name", { it }, { it }).nullable().default("{}")
    val toolNameToDescription = jsonb<String>("tool_name_to_description", { it }, { it }).nullable().default("{}")
    val pinnedTools = jsonb<String>("pinned_tools", { it }, { it }).nullable().default("{}")
    val extraHeaders = array("extra_headers", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val staticHeaders = jsonb<String>("static_headers", { it }, { it }).nullable().default("{}")
    val envVars = jsonb<String>("env_vars", { it }, { it }).nullable().default("[]")
    val status = text("status").nullable().default("unknown")
    val lastHealthCheck = pgTimestamp("last_health_check", 3).nullable()
    val healthCheckError = text("health_check_error").nullable()
    val command = text("command").nullable()
    val args = array("args", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val env = jsonb<String>("env", { it }, { it }).nullable().default("{}")
    val issuer = text("issuer").nullable()
    val authorizationUrl = text("authorization_url").nullable()
    val tokenUrl = text("token_url").nullable()
    val registrationUrl = text("registration_url").nullable()
    val oauth2Flow = text("oauth2_flow").nullable()
    val tokenExchangeEndpoint = text("token_exchange_endpoint").nullable()
    val audience = text("audience").nullable()
    val subjectTokenType = text("subject_token_type").nullable()
    val tokenExchangeProfile = text("token_exchange_profile").nullable()
    val allowAllKeys = bool("allow_all_keys").default(false)
    val availableOnPublicInternet = bool("available_on_public_internet").default(true)
    val delegateAuthToUpstream = bool("delegate_auth_to_upstream").default(false)
    val oauthPassthrough = bool("oauth_passthrough").default(false)
    val dcrBridge = bool("dcr_bridge").nullable()
    val perServerOauthDiscovery = bool("per_server_oauth_discovery").default(false)
    val isByok = bool("is_byok").default(false)
    val byokDescription = array("byok_description", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val byokApiKeyHelpUrl = text("byok_api_key_help_url").nullable()
    val sourceUrl = text("source_url").nullable()
    val timeout = double("timeout").nullable()
    val maxConcurrentRequests = integer("max_concurrent_requests").nullable()
    val rpm = integer("rpm").nullable()
    val approvalStatus = text("approval_status").nullable().default("active")
    val submittedBy = text("submitted_by").nullable()
    val submittedAt = pgTimestamp("submitted_at", 3).nullable()
    val reviewedAt = pgTimestamp("reviewed_at", 3).nullable()
    val reviewNotes = text("review_notes").nullable()
    override val primaryKey = PrimaryKey(id, name = "\"LiteLLM_MCPServerTable_pkey\"")

    init {
        index("\"LiteLLM_MCPServerTable_approval_status_idx\"", false, approvalStatus)
    }
}
