package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class CreditExpenseLogEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, CreditExpenseLogEntity>(CreditExpenseLogTable)

    var creditBalanceId by CreditExpenseLogTable.creditBalanceId
    var creditBalance by CreditBalanceEntity referencedOn CreditExpenseLogTable.creditBalanceId
    var bookingUid by CreditExpenseLogTable.bookingUid
    var booking by BookingEntity optionalReferencedOn CreditExpenseLogTable.bookingUid
    var credits by CreditExpenseLogTable.credits
    var creditType by CreditExpenseLogTable.creditType
    var date by CreditExpenseLogTable.date
    var smsSid by CreditExpenseLogTable.smsSid
    var smsSegments by CreditExpenseLogTable.smsSegments
    var phoneNumber by CreditExpenseLogTable.phoneNumber
    var email by CreditExpenseLogTable.email
    var callDuration by CreditExpenseLogTable.callDuration
    var creditFor by CreditExpenseLogTable.creditFor
    var externalRef by CreditExpenseLogTable.externalRef
}
