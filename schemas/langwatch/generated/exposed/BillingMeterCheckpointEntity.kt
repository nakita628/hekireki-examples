package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class BillingMeterCheckpointEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, BillingMeterCheckpointEntity>(BillingMeterCheckpointTable)

    var organizationId by BillingMeterCheckpointTable.organizationId
    var billingMonth by BillingMeterCheckpointTable.billingMonth
    var meter by BillingMeterCheckpointTable.meter
    var lastReportedTotal by BillingMeterCheckpointTable.lastReportedTotal
    var pendingReportedTotal by BillingMeterCheckpointTable.pendingReportedTotal
    var consecutiveFailures by BillingMeterCheckpointTable.consecutiveFailures
    var lastCountedEventId by BillingMeterCheckpointTable.lastCountedEventId
    var updatedAt by BillingMeterCheckpointTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(BillingMeterCheckpointTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
