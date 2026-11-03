package documenso.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class SubscriptionClaimEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, SubscriptionClaimEntity>(SubscriptionClaimTable)

    var createdAt by SubscriptionClaimTable.createdAt
    var updatedAt by SubscriptionClaimTable.updatedAt
    var name by SubscriptionClaimTable.name
    var locked by SubscriptionClaimTable.locked
    var teamCount by SubscriptionClaimTable.teamCount
    var memberCount by SubscriptionClaimTable.memberCount
    var envelopeItemCount by SubscriptionClaimTable.envelopeItemCount
    var recipientCount by SubscriptionClaimTable.recipientCount
    var flags by SubscriptionClaimTable.flags
    var documentRateLimits by SubscriptionClaimTable.documentRateLimits
    var documentQuota by SubscriptionClaimTable.documentQuota
    var emailRateLimits by SubscriptionClaimTable.emailRateLimits
    var emailQuota by SubscriptionClaimTable.emailQuota
    var apiRateLimits by SubscriptionClaimTable.apiRateLimits
    var apiQuota by SubscriptionClaimTable.apiQuota
    var emailTransportId by SubscriptionClaimTable.emailTransportId
    var emailTransport by EmailTransportEntity optionalReferencedOn SubscriptionClaimTable.emailTransportId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(SubscriptionClaimTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
