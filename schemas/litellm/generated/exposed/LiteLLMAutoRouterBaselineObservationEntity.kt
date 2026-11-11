package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class LiteLLMAutoRouterBaselineObservationEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LiteLLMAutoRouterBaselineObservationEntity>(LiteLLMAutoRouterBaselineObservationTable)

    var scope by LiteLLMAutoRouterBaselineObservationTable.scope
    var startedAt by LiteLLMAutoRouterBaselineObservationTable.startedAt
    var revision by LiteLLMAutoRouterBaselineObservationTable.revision
    var data by LiteLLMAutoRouterBaselineObservationTable.data
    var publication by LiteLLMAutoRouterBaselineObservationTable.publication
    var conflicted by LiteLLMAutoRouterBaselineObservationTable.conflicted
}
