package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class CreditBalanceEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, CreditBalanceEntity>(CreditBalanceTable)

    var team by TeamEntity optionalReferencedOn CreditBalanceTable.teamId
    var teamId by CreditBalanceTable.teamId
    var user by UserEntity optionalReferencedOn CreditBalanceTable.userId
    var userId by CreditBalanceTable.userId
    var additionalCredits by CreditBalanceTable.additionalCredits
    var limitReachedAt by CreditBalanceTable.limitReachedAt
    var warningSentAt by CreditBalanceTable.warningSentAt
    val expenseLogs by CreditExpenseLogEntity referrersOn CreditExpenseLogTable.creditBalanceId
    val purchaseLogs by CreditPurchaseLogEntity referrersOn CreditPurchaseLogTable.creditBalanceId
}
