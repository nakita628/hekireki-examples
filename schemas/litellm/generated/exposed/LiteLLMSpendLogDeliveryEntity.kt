package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class LiteLLMSpendLogDeliveryEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LiteLLMSpendLogDeliveryEntity>(LiteLLMSpendLogDeliveryTable)

    var requestId by LiteLLMSpendLogDeliveryTable.requestId
    var state by LiteLLMSpendLogDeliveryTable.state
    var accountingPayload by LiteLLMSpendLogDeliveryTable.accountingPayload
    var isBatch by LiteLLMSpendLogDeliveryTable.isBatch
    var attempts by LiteLLMSpendLogDeliveryTable.attempts
    var nextAttemptAt by LiteLLMSpendLogDeliveryTable.nextAttemptAt
    var appliedAt by LiteLLMSpendLogDeliveryTable.appliedAt
    var lastError by LiteLLMSpendLogDeliveryTable.lastError
    var createdAt by LiteLLMSpendLogDeliveryTable.createdAt
}
