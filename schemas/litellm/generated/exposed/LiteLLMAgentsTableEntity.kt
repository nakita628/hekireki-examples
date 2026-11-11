package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class LiteLLMAgentsTableEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LiteLLMAgentsTableEntity>(LiteLLMAgentsTableTable)

    var agentName by LiteLLMAgentsTableTable.agentName
    var litellmParams by LiteLLMAgentsTableTable.litellmParams
    var agentCardParams by LiteLLMAgentsTableTable.agentCardParams
    var staticHeaders by LiteLLMAgentsTableTable.staticHeaders
    var extraHeaders by LiteLLMAgentsTableTable.extraHeaders
    var killSwitch by LiteLLMAgentsTableTable.killSwitch
    var agentAccessGroups by LiteLLMAgentsTableTable.agentAccessGroups
    var accessGroupIds by LiteLLMAgentsTableTable.accessGroupIds
    var objectPermissionId by LiteLLMAgentsTableTable.objectPermissionId
    var objectPermission by LiteLLMObjectPermissionTableEntity optionalReferencedOn LiteLLMAgentsTableTable.objectPermissionId
    var spend by LiteLLMAgentsTableTable.spend
    var identityManaged by LiteLLMAgentsTableTable.identityManaged
    var enabled by LiteLLMAgentsTableTable.enabled
    var executionMode by LiteLLMAgentsTableTable.executionMode
    val identity by LiteLLMAgentIdentityEntity optionalBackReferencedOn LiteLLMAgentIdentityTable.id
    val retiredIdentities by LiteLLMRetiredAgentIdentityEntity optionalReferrersOn LiteLLMRetiredAgentIdentityTable.agentId
    var tpmLimit by LiteLLMAgentsTableTable.tpmLimit
    var rpmLimit by LiteLLMAgentsTableTable.rpmLimit
    var sessionTpmLimit by LiteLLMAgentsTableTable.sessionTpmLimit
    var sessionRpmLimit by LiteLLMAgentsTableTable.sessionRpmLimit
    var createdAt by LiteLLMAgentsTableTable.createdAt
    var createdBy by LiteLLMAgentsTableTable.createdBy
    var updatedAt by LiteLLMAgentsTableTable.updatedAt
    var updatedBy by LiteLLMAgentsTableTable.updatedBy

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(LiteLLMAgentsTableTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
