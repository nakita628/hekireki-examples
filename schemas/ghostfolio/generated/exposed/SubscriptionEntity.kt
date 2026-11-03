package ghostfolio.models

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

    var createdAt by SubscriptionTable.createdAt
    var expiresAt by SubscriptionTable.expiresAt
    var price by SubscriptionTable.price
    var stripeCheckoutSessionId by SubscriptionTable.stripeCheckoutSessionId
    var updatedAt by SubscriptionTable.updatedAt
    var user by UserEntity referencedOn SubscriptionTable.userId
    var userId by SubscriptionTable.userId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(SubscriptionTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
