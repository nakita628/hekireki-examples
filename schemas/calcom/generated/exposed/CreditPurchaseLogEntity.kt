package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class CreditPurchaseLogEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, CreditPurchaseLogEntity>(CreditPurchaseLogTable)

    var creditBalanceId by CreditPurchaseLogTable.creditBalanceId
    var creditBalance by CreditBalanceEntity referencedOn CreditPurchaseLogTable.creditBalanceId
    var credits by CreditPurchaseLogTable.credits
    var createdAt by CreditPurchaseLogTable.createdAt
}
