package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable

object LiteLLMSpendLogGuardrailIndexTable : CompositeIdTable("\"LiteLLM_SpendLogGuardrailIndex\"") {
    val requestId = text("request_id").entityId()
    val guardrailId = text("guardrail_id").entityId()
    val policyId = text("policy_id").nullable()
    val startTime = pgTimestamp("start_time", 3)
    override val primaryKey = PrimaryKey(requestId, guardrailId, name = "\"LiteLLM_SpendLogGuardrailIndex_pkey\"")

    init {
        index("\"LiteLLM_SpendLogGuardrailIndex_guardrail_id_start_time_idx\"", false, guardrailId, startTime)
        index("\"LiteLLM_SpendLogGuardrailIndex_policy_id_start_time_idx\"", false, policyId, startTime)
    }
}
