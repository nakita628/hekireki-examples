package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class LiteLLMInvitationLinkEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LiteLLMInvitationLinkEntity>(LiteLLMInvitationLinkTable)

    var userId by LiteLLMInvitationLinkTable.userId
    var isAccepted by LiteLLMInvitationLinkTable.isAccepted
    var acceptedAt by LiteLLMInvitationLinkTable.acceptedAt
    var expiresAt by LiteLLMInvitationLinkTable.expiresAt
    var createdAt by LiteLLMInvitationLinkTable.createdAt
    var createdBy by LiteLLMInvitationLinkTable.createdBy
    var updatedAt by LiteLLMInvitationLinkTable.updatedAt
    var updatedBy by LiteLLMInvitationLinkTable.updatedBy
    var liteLLMUserTableUser by LiteLLMUserTableEntity referencedOn LiteLLMInvitationLinkTable.userId
    var liteLLMUserTableCreated by LiteLLMUserTableEntity referencedOn LiteLLMInvitationLinkTable.createdBy
    var liteLLMUserTableUpdated by LiteLLMUserTableEntity referencedOn LiteLLMInvitationLinkTable.updatedBy
}
