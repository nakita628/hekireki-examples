package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class PaymentEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, PaymentEntity>(PaymentTable)

    var createdAt by PaymentTable.createdAt
    var updatedAt by PaymentTable.updatedAt
    var premiumId by PaymentTable.premiumId
    var premium by PremiumEntity optionalReferencedOn PaymentTable.premiumId
    var processorType by PaymentTable.processorType
    var processorId by PaymentTable.processorId
    var processorSubscriptionId by PaymentTable.processorSubscriptionId
    var processorCustomerId by PaymentTable.processorCustomerId
    var amount by PaymentTable.amount
    var currency by PaymentTable.currency
    var status by PaymentTable.status
    var tax by PaymentTable.tax
    var taxInclusive by PaymentTable.taxInclusive
    var refunded by PaymentTable.refunded
    var refundedAt by PaymentTable.refundedAt
    var refundedAmount by PaymentTable.refundedAmount
    var billingReason by PaymentTable.billingReason
    var invoiceEmailSentAt by PaymentTable.invoiceEmailSentAt
}
