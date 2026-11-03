package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class PayoutProblemsEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, PayoutProblemsEntity>(PayoutProblemsTable)

    var status by PayoutProblemsTable.status
    var orderId by PayoutProblemsTable.orderId
    var userId by PayoutProblemsTable.userId
    var postId by PayoutProblemsTable.postId
    var amount by PayoutProblemsTable.amount
    var createdAt by PayoutProblemsTable.createdAt
    var updatedAt by PayoutProblemsTable.updatedAt
    var order by OrdersEntity referencedOn PayoutProblemsTable.orderId
    var post by PostEntity optionalReferencedOn PayoutProblemsTable.postId
    var user by UserEntity referencedOn PayoutProblemsTable.userId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(PayoutProblemsTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
