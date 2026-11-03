package rallly.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class PaymentMethodEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, PaymentMethodEntity>(PaymentMethodTable)

    var userId by PaymentMethodTable.userId
    var type by PaymentMethodTable.type
    var data by PaymentMethodTable.data
    var createdAt by PaymentMethodTable.createdAt
    var updatedAt by PaymentMethodTable.updatedAt
    var user by UserEntity referencedOn PaymentMethodTable.userId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(PaymentMethodTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
