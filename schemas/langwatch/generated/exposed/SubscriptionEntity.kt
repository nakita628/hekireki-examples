package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class SubscriptionEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, SubscriptionEntity>(SubscriptionTable)

    var organizationId by SubscriptionTable.organizationId
    var plan by SubscriptionTable.plan
    var stripeSubscriptionId by SubscriptionTable.stripeSubscriptionId
    var status by SubscriptionTable.status
    var startDate by SubscriptionTable.startDate
    var endDate by SubscriptionTable.endDate
    var lastPaymentFailedDate by SubscriptionTable.lastPaymentFailedDate
    var createdAt by SubscriptionTable.createdAt
    var updatedAt by SubscriptionTable.updatedAt
    var maxMembers by SubscriptionTable.maxMembers
    var maxProjects by SubscriptionTable.maxProjects
    var maxMessagesPerMonth by SubscriptionTable.maxMessagesPerMonth
    var maxWorkflows by SubscriptionTable.maxWorkflows
    var maxRetentionDays by SubscriptionTable.maxRetentionDays
    var maxMembersLite by SubscriptionTable.maxMembersLite
    var maxMembersDeveloper by SubscriptionTable.maxMembersDeveloper
    var maxTeams by SubscriptionTable.maxTeams
    var maxPrompts by SubscriptionTable.maxPrompts
    var maxEvaluators by SubscriptionTable.maxEvaluators
    var maxScenarios by SubscriptionTable.maxScenarios
    var maxAgents by SubscriptionTable.maxAgents
    var maxExperiments by SubscriptionTable.maxExperiments
    var maxOnlineEvaluations by SubscriptionTable.maxOnlineEvaluations
    var maxDatasets by SubscriptionTable.maxDatasets
    var maxDashboards by SubscriptionTable.maxDashboards
    var maxCustomGraphs by SubscriptionTable.maxCustomGraphs
    var maxAutomations by SubscriptionTable.maxAutomations
    var seatsKept by SubscriptionTable.seatsKept
    var seatsKeptUntil by SubscriptionTable.seatsKeptUntil
    var seatLineQuantity by SubscriptionTable.seatLineQuantity
    var seatLineQuantityAt by SubscriptionTable.seatLineQuantityAt
    var seatLineWriteQuantities by SubscriptionTable.seatLineWriteQuantities
    var seatLineWriteSince by SubscriptionTable.seatLineWriteSince
    var seatLineWriteAt by SubscriptionTable.seatLineWriteAt
    var seatLineWriteAttempts by SubscriptionTable.seatLineWriteAttempts
    var seatLineWriteFlaggedAt by SubscriptionTable.seatLineWriteFlaggedAt
    var seatRepairQuantity by SubscriptionTable.seatRepairQuantity
    var seatRepairLiftedQuantity by SubscriptionTable.seatRepairLiftedQuantity
    var seatRepairSince by SubscriptionTable.seatRepairSince
    var seatRepairAttempts by SubscriptionTable.seatRepairAttempts
    var seatRepairFlaggedAt by SubscriptionTable.seatRepairFlaggedAt
    var seatLineLeaseToken by SubscriptionTable.seatLineLeaseToken
    var seatLineLeaseUntil by SubscriptionTable.seatLineLeaseUntil

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(SubscriptionTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
