package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class LiteLLMConfigEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LiteLLMConfigEntity>(LiteLLMConfigTable)

    var paramValue by LiteLLMConfigTable.paramValue
    var lastRunAt by LiteLLMConfigTable.lastRunAt
    var reloadRevision by LiteLLMConfigTable.reloadRevision
}
