package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class LiteLLMErrorLogsEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LiteLLMErrorLogsEntity>(LiteLLMErrorLogsTable)

    var startTime by LiteLLMErrorLogsTable.startTime
    var endTime by LiteLLMErrorLogsTable.endTime
    var apiBase by LiteLLMErrorLogsTable.apiBase
    var modelGroup by LiteLLMErrorLogsTable.modelGroup
    var litellmModelName by LiteLLMErrorLogsTable.litellmModelName
    var modelId by LiteLLMErrorLogsTable.modelId
    var requestKwargs by LiteLLMErrorLogsTable.requestKwargs
    var exceptionType by LiteLLMErrorLogsTable.exceptionType
    var exceptionString by LiteLLMErrorLogsTable.exceptionString
    var statusCode by LiteLLMErrorLogsTable.statusCode
}
