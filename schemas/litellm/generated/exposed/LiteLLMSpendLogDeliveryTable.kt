package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object LiteLLMSpendLogDeliveryTable : IdTable<String>("\"LiteLLM_SpendLogDelivery\"") {
    override val id = text("delivery_id").databaseDefault("(gen_random_uuid())::text").entityId()
    val requestId = text("request_id")
    val state =
        pgEnum("state", "\"AccountingDeliveryState\"", AccountingDeliveryState.entries, AccountingDeliveryState::dbName).default(
            AccountingDeliveryState.PENDING,
        )
    val accountingPayload = jsonb<String>("accounting_payload", { it }, { it })
    val isBatch = bool("is_batch")
    val attempts = integer("attempts").default(0)
    val nextAttemptAt =
        pgTimestamp("next_attempt_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val appliedAt = pgTimestamp("applied_at", 3).nullable()
    val lastError = text("last_error").nullable()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"LiteLLM_SpendLogDelivery_pkey\"")

    init {
        index("\"LiteLLM_SpendLogDelivery_state_next_attempt_at_idx\"", false, state, nextAttemptAt)
        index("\"LiteLLM_SpendLogDelivery_state_applied_at_idx\"", false, state, appliedAt)
        index("\"LiteLLM_SpendLogDelivery_request_id_idx\"", false, requestId)
    }
}
