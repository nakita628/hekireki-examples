package litellm.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.util.UUID

object LiteLLMAgentIdentityTable : IdTable<String>("\"LiteLLM_AgentIdentity\"") {
    override val id =
        text("agent_id")
            .references(
                LiteLLMAgentsTableTable.id,
                onDelete = ReferenceOption.CASCADE,
                onUpdate = ReferenceOption.CASCADE,
                fkName = "\"LiteLLM_AgentIdentity_agent_id_fkey\"",
            ).entityId()
    val active = bool("active").default(true)
    val provider = text("provider")
    val issuer = text("issuer")
    val tenantId = text("tenant_id")
    val clientId = text("client_id")
    val servicePrincipalId = text("service_principal_id").nullable()
    val requiredRoles = array("required_roles", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val requiredScopes =
        array("required_scopes", TextColumnType()).nullable().databaseDefault("'{\"user_impersonation\"}'") { listOf("user_impersonation") }
    val revision = text("revision").clientDefault { UUID.randomUUID().toString() }
    val lastAuthenticatedAt = pgTimestamp("last_authenticated_at", 3).nullable()
    override val primaryKey = PrimaryKey(id, name = "\"LiteLLM_AgentIdentity_pkey\"")

    init {
        uniqueIndex("\"LiteLLM_AgentIdentity_provider_tenant_id_client_id_key\"", provider, tenantId, clientId)
        uniqueIndex("\"LiteLLM_AgentIdentity_issuer_service_principal_id_key\"", issuer, servicePrincipalId)
    }
}
