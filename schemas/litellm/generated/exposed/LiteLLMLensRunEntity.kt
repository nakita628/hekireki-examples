package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class LiteLLMLensRunEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LiteLLMLensRunEntity>(LiteLLMLensRunTable)

    var lensId by LiteLLMLensRunTable.lensId
    var createdAt by LiteLLMLensRunTable.createdAt
    var data by LiteLLMLensRunTable.data
}
