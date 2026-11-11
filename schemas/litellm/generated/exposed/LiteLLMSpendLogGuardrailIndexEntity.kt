package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass

class LiteLLMSpendLogGuardrailIndexEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<LiteLLMSpendLogGuardrailIndexEntity>(LiteLLMSpendLogGuardrailIndexTable)

    val requestId by LiteLLMSpendLogGuardrailIndexTable.requestId
    val guardrailId by LiteLLMSpendLogGuardrailIndexTable.guardrailId
    var policyId by LiteLLMSpendLogGuardrailIndexTable.policyId
    var startTime by LiteLLMSpendLogGuardrailIndexTable.startTime
}
