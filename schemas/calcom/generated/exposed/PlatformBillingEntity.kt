package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class PlatformBillingEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, PlatformBillingEntity>(PlatformBillingTable)

    var customerId by PlatformBillingTable.customerId
    var subscriptionId by PlatformBillingTable.subscriptionId
    var priceId by PlatformBillingTable.priceId
    var plan by PlatformBillingTable.plan
    var billingCycleStart by PlatformBillingTable.billingCycleStart
    var billingCycleEnd by PlatformBillingTable.billingCycleEnd
    var overdue by PlatformBillingTable.overdue
    var managerBillingId by PlatformBillingTable.managerBillingId
    var managerBilling by PlatformBillingEntity optionalReferencedOn PlatformBillingTable.managerBillingId
    val managedBillings by PlatformBillingEntity optionalReferrersOn PlatformBillingTable.managerBillingId
    val team by TeamEntity referencedOn PlatformBillingTable.id
}
