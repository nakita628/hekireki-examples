package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class ConnectedInvoiceEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ConnectedInvoiceEntity>(ConnectedInvoiceTable)

    var accountId by ConnectedInvoiceTable.accountId
    var stripeInvoiceId by ConnectedInvoiceTable.stripeInvoiceId
    var kind by ConnectedInvoiceTable.kind
    var currency by ConnectedInvoiceTable.currency
    var amountCents by ConnectedInvoiceTable.amountCents
    var status by ConnectedInvoiceTable.status
    var paidOutOfBandAt by ConnectedInvoiceTable.paidOutOfBandAt
    var termStartsAt by ConnectedInvoiceTable.termStartsAt
    var createdAt by ConnectedInvoiceTable.createdAt
    var updatedAt by ConnectedInvoiceTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ConnectedInvoiceTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
