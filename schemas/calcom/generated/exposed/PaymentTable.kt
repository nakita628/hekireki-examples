package calcom.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb

object PaymentTable : IdTable<Int>("\"Payment\"") {
    override val id = integer("id").autoIncrement().entityId()
    val uid = text("uid").uniqueIndex("\"Payment_uid_key\"")
    val appId =
        optReference(
            "appId",
            AppTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Payment_appId_fkey\"",
        )
    val bookingId =
        reference(
            "bookingId",
            BookingTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Payment_bookingId_fkey\"",
        )
    val amount = integer("amount")
    val fee = integer("fee")
    val currency = text("currency")
    val success = bool("success")
    val refunded = bool("refunded")
    val data = jsonb<String>("data", { it }, { it })
    val externalId = text("externalId").uniqueIndex("\"Payment_externalId_key\"")
    val paymentOption =
        pgEnum("paymentOption", "\"PaymentOption\"", PaymentOption.entries, PaymentOption::dbName)
            .nullable()
            .default(PaymentOption.ON_BOOKING)
    override val primaryKey = PrimaryKey(id, name = "\"Payment_pkey\"")

    init {
        index("\"Payment_bookingId_idx\"", false, bookingId)
        index("\"Payment_externalId_idx\"", false, externalId)
    }
}
