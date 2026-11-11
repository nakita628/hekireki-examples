package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class LiteLLMSubscriptionAccountTableEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LiteLLMSubscriptionAccountTableEntity>(LiteLLMSubscriptionAccountTableTable)

    var customLlmProvider by LiteLLMSubscriptionAccountTableTable.customLlmProvider
    var accountId by LiteLLMSubscriptionAccountTableTable.accountId
    var label by LiteLLMSubscriptionAccountTableTable.label
    var monthlyFee by LiteLLMSubscriptionAccountTableTable.monthlyFee
    var currency by LiteLLMSubscriptionAccountTableTable.currency
    var billingPeriodStart by LiteLLMSubscriptionAccountTableTable.billingPeriodStart
    var createdAt by LiteLLMSubscriptionAccountTableTable.createdAt
    var createdBy by LiteLLMSubscriptionAccountTableTable.createdBy
    var updatedAt by LiteLLMSubscriptionAccountTableTable.updatedAt
    var updatedBy by LiteLLMSubscriptionAccountTableTable.updatedBy

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(LiteLLMSubscriptionAccountTableTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
