package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class ConnectedSeatChangeEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ConnectedSeatChangeEntity>(ConnectedSeatChangeTable)

    var licenseId by ConnectedSeatChangeTable.licenseId
    var accountId by ConnectedSeatChangeTable.accountId
    var organizationId by ConnectedSeatChangeTable.organizationId
    var changedAt by ConnectedSeatChangeTable.changedAt
    var addedSeats by ConnectedSeatChangeTable.addedSeats
    var unitAmountCents by ConnectedSeatChangeTable.unitAmountCents
    var amountCents by ConnectedSeatChangeTable.amountCents
    var currency by ConnectedSeatChangeTable.currency
    var stripeInvoiceId by ConnectedSeatChangeTable.stripeInvoiceId
    var state by ConnectedSeatChangeTable.state
    var createdAt by ConnectedSeatChangeTable.createdAt
    var updatedAt by ConnectedSeatChangeTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ConnectedSeatChangeTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
