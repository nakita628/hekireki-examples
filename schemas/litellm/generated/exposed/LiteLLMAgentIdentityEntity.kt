package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class LiteLLMAgentIdentityEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LiteLLMAgentIdentityEntity>(LiteLLMAgentIdentityTable)

    var active by LiteLLMAgentIdentityTable.active
    val agent by LiteLLMAgentsTableEntity referencedOn LiteLLMAgentIdentityTable.id
    var provider by LiteLLMAgentIdentityTable.provider
    var issuer by LiteLLMAgentIdentityTable.issuer
    var tenantId by LiteLLMAgentIdentityTable.tenantId
    var clientId by LiteLLMAgentIdentityTable.clientId
    var servicePrincipalId by LiteLLMAgentIdentityTable.servicePrincipalId
    var requiredRoles by LiteLLMAgentIdentityTable.requiredRoles
    var requiredScopes by LiteLLMAgentIdentityTable.requiredScopes
    var revision by LiteLLMAgentIdentityTable.revision
    var lastAuthenticatedAt by LiteLLMAgentIdentityTable.lastAuthenticatedAt
}
