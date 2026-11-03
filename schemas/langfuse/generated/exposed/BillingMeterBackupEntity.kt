package langfuse.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import java.time.Instant
import java.time.temporal.ChronoUnit

class BillingMeterBackupEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<BillingMeterBackupEntity>(BillingMeterBackupTable)

    val stripeCustomerId by BillingMeterBackupTable.stripeCustomerId
    val meterId by BillingMeterBackupTable.meterId
    val startTime by BillingMeterBackupTable.startTime
    val endTime by BillingMeterBackupTable.endTime
    var aggregatedValue by BillingMeterBackupTable.aggregatedValue
    var eventName by BillingMeterBackupTable.eventName
    var orgId by BillingMeterBackupTable.orgId
    var createdAt by BillingMeterBackupTable.createdAt
    var updatedAt by BillingMeterBackupTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(BillingMeterBackupTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
