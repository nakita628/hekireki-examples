package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable

object LiteLLMSpendLogToolIndexTable : CompositeIdTable("\"LiteLLM_SpendLogToolIndex\"") {
    val requestId = text("request_id").entityId()
    val toolName = text("tool_name").entityId()
    val startTime = pgTimestamp("start_time", 3)
    override val primaryKey = PrimaryKey(requestId, toolName, name = "\"LiteLLM_SpendLogToolIndex_pkey\"")

    init {
        index("\"LiteLLM_SpendLogToolIndex_tool_name_start_time_idx\"", false, toolName, startTime)
        index("\"LiteLLM_SpendLogToolIndex_start_time_idx\"", false, startTime)
    }
}
