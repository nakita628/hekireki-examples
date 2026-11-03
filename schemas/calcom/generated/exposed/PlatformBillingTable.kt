package calcom.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable

object PlatformBillingTable : IdTable<Int>("\"PlatformBilling\"") {
    override val id =
        integer("id")
            .references(
                TeamTable.id,
                onDelete = ReferenceOption.CASCADE,
                onUpdate = ReferenceOption.CASCADE,
                fkName = "\"PlatformBilling_id_fkey\"",
            ).entityId()
    val customerId = text("customerId")
    val subscriptionId = text("subscriptionId").nullable()
    val priceId = text("priceId").nullable()
    val plan = text("plan").default("none")
    val billingCycleStart = integer("billingCycleStart").nullable()
    val billingCycleEnd = integer("billingCycleEnd").nullable()
    val overdue = bool("overdue").nullable().default(false)
    val managerBillingId =
        optReference(
            "managerBillingId",
            PlatformBillingTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"PlatformBilling_managerBillingId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"PlatformBilling_pkey\"")

    init {
        uniqueIndex("\"PlatformBilling_id_key\"", id)
    }
}
