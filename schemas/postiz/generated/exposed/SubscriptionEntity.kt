package postiz.models

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
    var subscriptionTier by SubscriptionTable.subscriptionTier
    var provider by SubscriptionTable.provider
    var identifier by SubscriptionTable.identifier
    var cancelAt by SubscriptionTable.cancelAt
    var period by SubscriptionTable.period
    var totalChannels by SubscriptionTable.totalChannels
    var isLifetime by SubscriptionTable.isLifetime
    var createdAt by SubscriptionTable.createdAt
    var updatedAt by SubscriptionTable.updatedAt
    var deletedAt by SubscriptionTable.deletedAt
    var organization by OrganizationEntity referencedOn SubscriptionTable.organizationId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(SubscriptionTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
