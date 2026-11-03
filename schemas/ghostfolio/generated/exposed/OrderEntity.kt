package ghostfolio.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class OrderEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, OrderEntity>(OrderTable)

    var account by AccountEntity optionalReferencedOn OrderTable
    var accountId by OrderTable.accountId
    var accountUserId by OrderTable.accountUserId
    var comment by OrderTable.comment
    var createdAt by OrderTable.createdAt
    var currency by OrderTable.currency
    var date by OrderTable.date
    var fee by OrderTable.fee
    var quantity by OrderTable.quantity
    var symbolProfileId by OrderTable.symbolProfileId
    var tags by TagEntity via OrderToTagTable
    var type by OrderTable.type
    var unitPrice by OrderTable.unitPrice
    var updatedAt by OrderTable.updatedAt
    var user by UserEntity referencedOn OrderTable.userId
    var userId by OrderTable.userId
    var symbolProfile by SymbolProfileEntity referencedOn OrderTable.symbolProfileId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(OrderTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
