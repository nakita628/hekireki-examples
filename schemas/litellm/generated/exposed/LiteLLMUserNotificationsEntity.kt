package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class LiteLLMUserNotificationsEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LiteLLMUserNotificationsEntity>(LiteLLMUserNotificationsTable)

    var userId by LiteLLMUserNotificationsTable.userId
    var models by LiteLLMUserNotificationsTable.models
    var justification by LiteLLMUserNotificationsTable.justification
    var status by LiteLLMUserNotificationsTable.status
}
