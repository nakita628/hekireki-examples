package documenso.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class SubscriptionEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, SubscriptionEntity>(SubscriptionTable)

    var status by SubscriptionTable.status
    var planId by SubscriptionTable.planId
    var priceId by SubscriptionTable.priceId
    var periodEnd by SubscriptionTable.periodEnd
    var createdAt by SubscriptionTable.createdAt
    var updatedAt by SubscriptionTable.updatedAt
    var cancelAtPeriodEnd by SubscriptionTable.cancelAtPeriodEnd
    var customerId by SubscriptionTable.customerId
    var organisationId by SubscriptionTable.organisationId
    var organisation by OrganisationEntity referencedOn SubscriptionTable.organisationId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(SubscriptionTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
