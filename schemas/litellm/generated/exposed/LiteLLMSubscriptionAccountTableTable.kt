package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object LiteLLMSubscriptionAccountTableTable : IdTable<String>("\"LiteLLM_SubscriptionAccountTable\"") {
    override val id = text("subscription_account_id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val customLlmProvider = text("custom_llm_provider")
    val accountId = text("account_id")
    val label = text("label").nullable()
    val monthlyFee = double("monthly_fee")
    val currency = text("currency")
    val billingPeriodStart = text("billing_period_start")
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val createdBy = text("created_by")
    val updatedAt = pgTimestamp("updated_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedBy = text("updated_by")
    override val primaryKey = PrimaryKey(id, name = "\"LiteLLM_SubscriptionAccountTable_pkey\"")

    init {
        uniqueIndex("\"LiteLLM_SubscriptionAccountTable_provider_account_key\"", customLlmProvider, accountId)
    }
}
