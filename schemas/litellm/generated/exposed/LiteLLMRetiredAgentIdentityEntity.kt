package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class LiteLLMRetiredAgentIdentityEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LiteLLMRetiredAgentIdentityEntity>(LiteLLMRetiredAgentIdentityTable)

    var agentId by LiteLLMRetiredAgentIdentityTable.agentId
    var agent by LiteLLMAgentsTableEntity optionalReferencedOn LiteLLMRetiredAgentIdentityTable.agentId
    var provider by LiteLLMRetiredAgentIdentityTable.provider
    var issuer by LiteLLMRetiredAgentIdentityTable.issuer
    var tenantId by LiteLLMRetiredAgentIdentityTable.tenantId
    var clientId by LiteLLMRetiredAgentIdentityTable.clientId
}
