package litellm.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.util.UUID

object LiteLLMRetiredAgentIdentityTable : IdTable<String>("\"LiteLLM_RetiredAgentIdentity\"") {
    override val id = text("binding_id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val agentId =
        optReference(
            "agent_id",
            LiteLLMAgentsTableTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"LiteLLM_RetiredAgentIdentity_agent_id_fkey\"",
        )
    val provider = text("provider")
    val issuer = text("issuer")
    val tenantId = text("tenant_id")
    val clientId = text("client_id")
    override val primaryKey = PrimaryKey(id, name = "\"LiteLLM_RetiredAgentIdentity_pkey\"")

    init {
        uniqueIndex("\"LiteLLM_RetiredAgentIdentity_provider_tenant_id_client_id_key\"", provider, tenantId, clientId)
    }
}
