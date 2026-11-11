package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class LiteLLMPolicyAttachmentTableEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LiteLLMPolicyAttachmentTableEntity>(LiteLLMPolicyAttachmentTableTable)

    var policyName by LiteLLMPolicyAttachmentTableTable.policyName
    var scope by LiteLLMPolicyAttachmentTableTable.scope
    var teams by LiteLLMPolicyAttachmentTableTable.teams
    var keys by LiteLLMPolicyAttachmentTableTable.keys
    var models by LiteLLMPolicyAttachmentTableTable.models
    var tags by LiteLLMPolicyAttachmentTableTable.tags
    var priority by LiteLLMPolicyAttachmentTableTable.priority
    var isDefault by LiteLLMPolicyAttachmentTableTable.isDefault
    var createdAt by LiteLLMPolicyAttachmentTableTable.createdAt
    var createdBy by LiteLLMPolicyAttachmentTableTable.createdBy
    var updatedAt by LiteLLMPolicyAttachmentTableTable.updatedAt
    var updatedBy by LiteLLMPolicyAttachmentTableTable.updatedBy

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(LiteLLMPolicyAttachmentTableTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
