package litellm.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object LiteLLMCustomerBudgetIncreaseRequestTable : IdTable<String>("\"LiteLLM_CustomerBudgetIncreaseRequest\"") {
    override val id = text("request_id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val customerMemberId =
        reference(
            "customer_member_id",
            LiteLLMCustomerMemberTableTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"LiteLLM_CustomerBudgetIncreaseRequest_customer_member_id_fkey\"",
        )
    val requestedMaxBudget = double("requested_max_budget")
    val expiresAt = pgTimestamp("expires_at", 3).nullable()
    val reason = text("reason").nullable()
    val status = text("status").default("pending_review")
    val submittedBy = text("submitted_by")
    val submittedAt = pgTimestamp("submitted_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val reviewedBy = text("reviewed_by").nullable()
    val reviewedAt = pgTimestamp("reviewed_at", 3).nullable()
    val reviewNotes = text("review_notes").nullable()
    override val primaryKey = PrimaryKey(id, name = "\"LiteLLM_CustomerBudgetIncreaseRequest_pkey\"")

    init {
        index("\"LiteLLM_CustomerBudgetIncreaseRequest_status_idx\"", false, status)
        index("\"LiteLLM_CustomerBudgetIncreaseRequest_customer_member_id_idx\"", false, customerMemberId)
    }
}
