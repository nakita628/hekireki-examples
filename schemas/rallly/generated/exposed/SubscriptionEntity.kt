package rallly.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class SubscriptionEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, SubscriptionEntity>(SubscriptionTable)

    var priceId by SubscriptionTable.priceId
    var quantity by SubscriptionTable.quantity
    var subscriptionItemId by SubscriptionTable.subscriptionItemId
    var amount by SubscriptionTable.amount
    var discountPercentOff by SubscriptionTable.discountPercentOff
    var discountAmountOff by SubscriptionTable.discountAmountOff
    var status by SubscriptionTable.status
    var active by SubscriptionTable.active
    var currency by SubscriptionTable.currency
    var interval by SubscriptionTable.interval
    var createdAt by SubscriptionTable.createdAt
    var periodStart by SubscriptionTable.periodStart
    var periodEnd by SubscriptionTable.periodEnd
    var cancelAtPeriodEnd by SubscriptionTable.cancelAtPeriodEnd
    var userId by SubscriptionTable.userId
    var spaceId by SubscriptionTable.spaceId
    var apiAddonItemId by SubscriptionTable.apiAddonItemId
    var apiAddonPriceId by SubscriptionTable.apiAddonPriceId
    var apiAddonAmount by SubscriptionTable.apiAddonAmount
    var apiAddonTier by SubscriptionTable.apiAddonTier
    var apiPollRetainedTier by SubscriptionTable.apiPollRetainedTier
    var apiPollRetainedUntil by SubscriptionTable.apiPollRetainedUntil
    var user by UserEntity referencedOn SubscriptionTable.userId
    var space by SpaceEntity referencedOn SubscriptionTable.spaceId
}
