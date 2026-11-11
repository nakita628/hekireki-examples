package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class LiteLLMProxyWorkerHeartbeatEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LiteLLMProxyWorkerHeartbeatEntity>(LiteLLMProxyWorkerHeartbeatTable)

    var hostname by LiteLLMProxyWorkerHeartbeatTable.hostname
    var startedAt by LiteLLMProxyWorkerHeartbeatTable.startedAt
    var lastHeartbeatAt by LiteLLMProxyWorkerHeartbeatTable.lastHeartbeatAt
}
