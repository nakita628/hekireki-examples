package inboxzero.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable

object PaymentTable : IdTable<String>("\"Payment\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("createdAt", 3)
    val updatedAt = pgTimestamp("updatedAt", 3)
    val premiumId =
        optReference(
            "premiumId",
            PremiumTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Payment_premiumId_fkey\"",
        )
    val processorType =
        pgEnum("processorType", "\"ProcessorType\"", ProcessorType.entries, ProcessorType::dbName).default(ProcessorType.LEMON_SQUEEZY)
    val processorId = text("processorId").nullable().uniqueIndex("\"Payment_processorId_key\"")
    val processorSubscriptionId = text("processorSubscriptionId").nullable()
    val processorCustomerId = text("processorCustomerId").nullable()
    val amount = integer("amount")
    val currency = text("currency")
    val status = text("status")
    val tax = integer("tax")
    val taxInclusive = bool("taxInclusive")
    val refunded = bool("refunded").default(false)
    val refundedAt = pgTimestamp("refundedAt", 3).nullable()
    val refundedAmount = integer("refundedAmount").nullable()
    val billingReason = text("billingReason").nullable()
    val invoiceEmailSentAt = pgTimestamp("invoiceEmailSentAt", 3).nullable()
    override val primaryKey = PrimaryKey(id, name = "\"Payment_pkey\"")

    init {
        index("\"Payment_premiumId_createdAt_idx\"", false, premiumId, createdAt)
    }
}
