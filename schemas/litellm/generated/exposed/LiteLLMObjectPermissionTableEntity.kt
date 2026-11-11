package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class LiteLLMObjectPermissionTableEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LiteLLMObjectPermissionTableEntity>(LiteLLMObjectPermissionTableTable)

    var mcpServers by LiteLLMObjectPermissionTableTable.mcpServers
    var mcpAccessGroups by LiteLLMObjectPermissionTableTable.mcpAccessGroups
    var mcpToolPermissions by LiteLLMObjectPermissionTableTable.mcpToolPermissions
    var vectorStores by LiteLLMObjectPermissionTableTable.vectorStores
    var agents by LiteLLMObjectPermissionTableTable.agents
    var agentAccessGroups by LiteLLMObjectPermissionTableTable.agentAccessGroups
    var models by LiteLLMObjectPermissionTableTable.models
    var blockedTools by LiteLLMObjectPermissionTableTable.blockedTools
    var mcpToolsets by LiteLLMObjectPermissionTableTable.mcpToolsets
    var searchTools by LiteLLMObjectPermissionTableTable.searchTools
    var mcpToolSearchEnabled by LiteLLMObjectPermissionTableTable.mcpToolSearchEnabled
    var skills by LiteLLMObjectPermissionTableTable.skills
    val teams by LiteLLMTeamTableEntity optionalReferrersOn LiteLLMTeamTableTable.objectPermissionId
    val projects by LiteLLMProjectTableEntity optionalReferrersOn LiteLLMProjectTableTable.objectPermissionId
    val verificationTokens by LiteLLMVerificationTokenEntity optionalReferrersOn LiteLLMVerificationTokenTable.objectPermissionId
    val organizations by LiteLLMOrganizationTableEntity optionalReferrersOn LiteLLMOrganizationTableTable.objectPermissionId
    val users by LiteLLMUserTableEntity optionalReferrersOn LiteLLMUserTableTable.objectPermissionId
    val endUsers by LiteLLMEndUserTableEntity optionalReferrersOn LiteLLMEndUserTableTable.objectPermissionId
    val agentsTable by LiteLLMAgentsTableEntity optionalReferrersOn LiteLLMAgentsTableTable.objectPermissionId
}
