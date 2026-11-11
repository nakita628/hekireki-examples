package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class LiteLLMCronJobEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LiteLLMCronJobEntity>(LiteLLMCronJobTable)

    var podId by LiteLLMCronJobTable.podId
    var status by LiteLLMCronJobTable.status
    var lastUpdated by LiteLLMCronJobTable.lastUpdated
    var ttl by LiteLLMCronJobTable.ttl
}
