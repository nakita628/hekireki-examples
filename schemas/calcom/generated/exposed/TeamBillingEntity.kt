package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class TeamBillingEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, TeamBillingEntity>(TeamBillingTable)

    var teamId by TeamBillingTable.teamId
    var team by TeamEntity referencedOn TeamBillingTable.teamId
    var subscriptionId by TeamBillingTable.subscriptionId
    var subscriptionItemId by TeamBillingTable.subscriptionItemId
    var customerId by TeamBillingTable.customerId
    var status by TeamBillingTable.status
    var planName by TeamBillingTable.planName
    var subscriptionStart by TeamBillingTable.subscriptionStart
    var subscriptionTrialEnd by TeamBillingTable.subscriptionTrialEnd
    var subscriptionEnd by TeamBillingTable.subscriptionEnd
    var billingPeriod by TeamBillingTable.billingPeriod
    var billingMode by TeamBillingTable.billingMode
    var pricePerSeat by TeamBillingTable.pricePerSeat
    var paidSeats by TeamBillingTable.paidSeats
    val monthlyProrations by MonthlyProrationEntity optionalReferrersOn MonthlyProrationTable.teamBillingId
    val seatChangeLogs by SeatChangeLogEntity optionalReferrersOn SeatChangeLogTable.teamBillingId
    var highWaterMark by TeamBillingTable.highWaterMark
    var highWaterMarkPeriodStart by TeamBillingTable.highWaterMarkPeriodStart
    var createdAt by TeamBillingTable.createdAt
    var updatedAt by TeamBillingTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(TeamBillingTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
