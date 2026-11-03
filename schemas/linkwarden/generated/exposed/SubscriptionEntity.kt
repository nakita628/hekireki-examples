package linkwarden.models

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

    var active by SubscriptionTable.active
    var provider by SubscriptionTable.provider
    var currentPeriodStart by SubscriptionTable.currentPeriodStart
    var currentPeriodEnd by SubscriptionTable.currentPeriodEnd
    var quantity by SubscriptionTable.quantity
    var storeOriginalTransactionId by SubscriptionTable.storeOriginalTransactionId
    var storeProductId by SubscriptionTable.storeProductId
    var googlePurchaseToken by SubscriptionTable.googlePurchaseToken
    var storeMetadata by SubscriptionTable.storeMetadata
    var stripeSubscriptionId by SubscriptionTable.stripeSubscriptionId
    var user by UserEntity referencedOn SubscriptionTable.userId
    var userId by SubscriptionTable.userId
    val childUsers by UserEntity optionalReferrersOn UserTable.parentSubscriptionId
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
