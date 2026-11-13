package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class LiteLLMScheduledJobRunEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LiteLLMScheduledJobRunEntity>(LiteLLMScheduledJobRunTable)

    var jobName by LiteLLMScheduledJobRunTable.jobName
    var lockGroup by LiteLLMScheduledJobRunTable.lockGroup
    var schedule by LiteLLMScheduledJobRunTable.schedule
    var scheduledFor by LiteLLMScheduledJobRunTable.scheduledFor
    var status by LiteLLMScheduledJobRunTable.status
    var claimedBy by LiteLLMScheduledJobRunTable.claimedBy
    var claimedAt by LiteLLMScheduledJobRunTable.claimedAt
    var leaseExpiresAt by LiteLLMScheduledJobRunTable.leaseExpiresAt
    var finishedAt by LiteLLMScheduledJobRunTable.finishedAt
    var createdAt by LiteLLMScheduledJobRunTable.createdAt
}
