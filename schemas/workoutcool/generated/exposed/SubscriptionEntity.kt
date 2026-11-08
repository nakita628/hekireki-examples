package workoutcool.models

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

    var userId by SubscriptionTable.userId
    var user by UserEntity referencedOn SubscriptionTable.userId
    var planId by SubscriptionTable.planId
    var plan by SubscriptionPlanEntity referencedOn SubscriptionTable.planId
    var revenueCatUserId by SubscriptionTable.revenueCatUserId
    var status by SubscriptionTable.status
    var startedAt by SubscriptionTable.startedAt
    var currentPeriodEnd by SubscriptionTable.currentPeriodEnd
    var cancelledAt by SubscriptionTable.cancelledAt
    var platform by SubscriptionTable.platform
    var createdAt by SubscriptionTable.createdAt
    var updatedAt by SubscriptionTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(SubscriptionTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
