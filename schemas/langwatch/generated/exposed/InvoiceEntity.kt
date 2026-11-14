package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class InvoiceEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, InvoiceEntity>(InvoiceTable)

    var subscriptionId by InvoiceTable.subscriptionId
    var totalAmount by InvoiceTable.totalAmount
    var currency by InvoiceTable.currency
    var dueDate by InvoiceTable.dueDate
    var paid by InvoiceTable.paid
    var createdAt by InvoiceTable.createdAt
    var updatedAt by InvoiceTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(InvoiceTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
