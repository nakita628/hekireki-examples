package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.util.UUID

object LiteLLMErrorLogsTable : IdTable<String>("\"LiteLLM_ErrorLogs\"") {
    override val id = text("request_id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val startTime = pgTimestamp("startTime", 3)
    val endTime = pgTimestamp("endTime", 3)
    val apiBase = text("api_base").default("")
    val modelGroup = text("model_group").default("")
    val litellmModelName = text("litellm_model_name").default("")
    val modelId = text("model_id").default("")
    val requestKwargs = jsonb<String>("request_kwargs", { it }, { it }).default("{}")
    val exceptionType = text("exception_type").default("")
    val exceptionString = text("exception_string").default("")
    val statusCode = text("status_code").default("")
    override val primaryKey = PrimaryKey(id, name = "\"LiteLLM_ErrorLogs_pkey\"")
}
