package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class OrdersEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, OrdersEntity>(OrdersTable)

    var buyerId by OrdersTable.buyerId
    var sellerId by OrdersTable.sellerId
    var status by OrdersTable.status
    var messageGroupId by OrdersTable.messageGroupId
    var captureId by OrdersTable.captureId
    var createdAt by OrdersTable.createdAt
    var updatedAt by OrdersTable.updatedAt
    val ordersItems by OrderItemsEntity referrersOn OrderItemsTable.orderId
    var buyer by UserEntity referencedOn OrdersTable.buyerId
    var messageGroup by MessagesGroupEntity referencedOn OrdersTable.messageGroupId
    var seller by UserEntity referencedOn OrdersTable.sellerId
    val payoutProblems by PayoutProblemsEntity referrersOn PayoutProblemsTable.orderId
    val posts by PostEntity optionalReferrersOn PostTable.submittedForOrderId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(OrdersTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
