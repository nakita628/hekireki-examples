package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class MonthlyProrationEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, MonthlyProrationEntity>(MonthlyProrationTable)

    var teamId by MonthlyProrationTable.teamId
    var team by TeamEntity referencedOn MonthlyProrationTable.teamId
    var monthKey by MonthlyProrationTable.monthKey
    var periodStart by MonthlyProrationTable.periodStart
    var periodEnd by MonthlyProrationTable.periodEnd
    var seatsAtStart by MonthlyProrationTable.seatsAtStart
    var seatsAdded by MonthlyProrationTable.seatsAdded
    var seatsRemoved by MonthlyProrationTable.seatsRemoved
    var netSeatIncrease by MonthlyProrationTable.netSeatIncrease
    var seatsAtEnd by MonthlyProrationTable.seatsAtEnd
    var subscriptionId by MonthlyProrationTable.subscriptionId
    var subscriptionItemId by MonthlyProrationTable.subscriptionItemId
    var customerId by MonthlyProrationTable.customerId
    var subscriptionStart by MonthlyProrationTable.subscriptionStart
    var subscriptionEnd by MonthlyProrationTable.subscriptionEnd
    var remainingDays by MonthlyProrationTable.remainingDays
    var pricePerSeat by MonthlyProrationTable.pricePerSeat
    var proratedAmount by MonthlyProrationTable.proratedAmount
    var invoiceItemId by MonthlyProrationTable.invoiceItemId
    var invoiceId by MonthlyProrationTable.invoiceId
    var invoiceUrl by MonthlyProrationTable.invoiceUrl
    var status by MonthlyProrationTable.status
    var chargedAt by MonthlyProrationTable.chargedAt
    var failedAt by MonthlyProrationTable.failedAt
    var failureReason by MonthlyProrationTable.failureReason
    var retryCount by MonthlyProrationTable.retryCount
    val seatChanges by SeatChangeLogEntity optionalReferrersOn SeatChangeLogTable.processedInProrationId
    var metadata by MonthlyProrationTable.metadata
    var createdAt by MonthlyProrationTable.createdAt
    var updatedAt by MonthlyProrationTable.updatedAt
    var teamBillingId by MonthlyProrationTable.teamBillingId
    var teamBilling by TeamBillingEntity optionalReferencedOn MonthlyProrationTable.teamBillingId
    var organizationBillingId by MonthlyProrationTable.organizationBillingId
    var organizationBilling by OrganizationBillingEntity optionalReferencedOn MonthlyProrationTable.organizationBillingId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(MonthlyProrationTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
