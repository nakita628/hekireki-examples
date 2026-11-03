package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class OrganizationBillingEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, OrganizationBillingEntity>(OrganizationBillingTable)

    var teamId by OrganizationBillingTable.teamId
    var team by TeamEntity referencedOn OrganizationBillingTable.teamId
    var subscriptionId by OrganizationBillingTable.subscriptionId
    var subscriptionItemId by OrganizationBillingTable.subscriptionItemId
    var customerId by OrganizationBillingTable.customerId
    var status by OrganizationBillingTable.status
    var planName by OrganizationBillingTable.planName
    var subscriptionStart by OrganizationBillingTable.subscriptionStart
    var subscriptionTrialEnd by OrganizationBillingTable.subscriptionTrialEnd
    var subscriptionEnd by OrganizationBillingTable.subscriptionEnd
    var billingPeriod by OrganizationBillingTable.billingPeriod
    var billingMode by OrganizationBillingTable.billingMode
    var pricePerSeat by OrganizationBillingTable.pricePerSeat
    var paidSeats by OrganizationBillingTable.paidSeats
    val monthlyProrations by MonthlyProrationEntity optionalReferrersOn MonthlyProrationTable.organizationBillingId
    val seatChangeLogs by SeatChangeLogEntity optionalReferrersOn SeatChangeLogTable.organizationBillingId
    var highWaterMark by OrganizationBillingTable.highWaterMark
    var highWaterMarkPeriodStart by OrganizationBillingTable.highWaterMarkPeriodStart
    var createdAt by OrganizationBillingTable.createdAt
    var updatedAt by OrganizationBillingTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(OrganizationBillingTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
