package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class PaymentEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, PaymentEntity>(PaymentTable)

    var uid by PaymentTable.uid
    var app by AppEntity optionalReferencedOn PaymentTable.appId
    var appId by PaymentTable.appId
    var bookingId by PaymentTable.bookingId
    var booking by BookingEntity referencedOn PaymentTable.bookingId
    var amount by PaymentTable.amount
    var fee by PaymentTable.fee
    var currency by PaymentTable.currency
    var success by PaymentTable.success
    var refunded by PaymentTable.refunded
    var data by PaymentTable.data
    var externalId by PaymentTable.externalId
    var paymentOption by PaymentTable.paymentOption
}
