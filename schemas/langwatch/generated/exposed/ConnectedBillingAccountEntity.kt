package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class ConnectedBillingAccountEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ConnectedBillingAccountEntity>(ConnectedBillingAccountTable)

    var organizationId by ConnectedBillingAccountTable.organizationId
    var stripeCustomerId by ConnectedBillingAccountTable.stripeCustomerId
    var usageSubscriptionId by ConnectedBillingAccountTable.usageSubscriptionId
    var usageSubscriptionItemId by ConnectedBillingAccountTable.usageSubscriptionItemId
    var termStartsAt by ConnectedBillingAccountTable.termStartsAt
    var termEndsAt by ConnectedBillingAccountTable.termEndsAt
    var commitUsdCents by ConnectedBillingAccountTable.commitUsdCents
    var seatCurrency by ConnectedBillingAccountTable.seatCurrency
    var seatRateCents by ConnectedBillingAccountTable.seatRateCents
    var seats by ConnectedBillingAccountTable.seats
    var bankTransferType by ConnectedBillingAccountTable.bankTransferType
    var bankTransferCountry by ConnectedBillingAccountTable.bankTransferCountry
    var billingEmail by ConnectedBillingAccountTable.billingEmail
    var pendingRenewal by ConnectedBillingAccountTable.pendingRenewal
    var createdById by ConnectedBillingAccountTable.createdById
    var createdAt by ConnectedBillingAccountTable.createdAt
    var updatedAt by ConnectedBillingAccountTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ConnectedBillingAccountTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
