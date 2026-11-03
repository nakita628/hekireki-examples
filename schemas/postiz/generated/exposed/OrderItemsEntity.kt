package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class OrderItemsEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, OrderItemsEntity>(OrderItemsTable)

    var orderId by OrderItemsTable.orderId
    var integrationId by OrderItemsTable.integrationId
    var quantity by OrderItemsTable.quantity
    var price by OrderItemsTable.price
    var integration by IntegrationEntity referencedOn OrderItemsTable.integrationId
    var order by OrdersEntity referencedOn OrderItemsTable.orderId
}
