package workoutcool.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class SubscriptionPlanEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, SubscriptionPlanEntity>(SubscriptionPlanTable)

    var priceMonthly by SubscriptionPlanTable.priceMonthly
    var priceYearly by SubscriptionPlanTable.priceYearly
    var currency by SubscriptionPlanTable.currency
    var interval by SubscriptionPlanTable.interval
    var isActive by SubscriptionPlanTable.isActive
    var availableRegions by SubscriptionPlanTable.availableRegions
    var createdAt by SubscriptionPlanTable.createdAt
    var updatedAt by SubscriptionPlanTable.updatedAt
    val subscriptions by SubscriptionEntity referrersOn SubscriptionTable.planId
    val providerMappings by PlanProviderMappingEntity referrersOn PlanProviderMappingTable.planId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(SubscriptionPlanTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
