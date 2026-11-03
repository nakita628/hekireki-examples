package postiz.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.util.UUID

object OrderItemsTable : IdTable<String>("\"OrderItems\"") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val orderId =
        reference(
            "orderId",
            OrdersTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"OrderItems_orderId_fkey\"",
        )
    val integrationId =
        reference(
            "integrationId",
            IntegrationTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"OrderItems_integrationId_fkey\"",
        )
    val quantity = integer("quantity")
    val price = integer("price")
    override val primaryKey = PrimaryKey(id, name = "\"OrderItems_pkey\"")

    init {
        index("\"OrderItems_orderId_idx\"", false, orderId)
        index("\"OrderItems_integrationId_idx\"", false, integrationId)
    }
}
