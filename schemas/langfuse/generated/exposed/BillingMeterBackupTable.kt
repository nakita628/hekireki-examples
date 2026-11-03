package langfuse.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object BillingMeterBackupTable : CompositeIdTable("billing_meter_backups") {
    val stripeCustomerId = text("stripe_customer_id").entityId()
    val meterId = text("meter_id").entityId()
    val startTime = pgTimestamp("start_time", 3).entityId()
    val endTime = pgTimestamp("end_time", 3).entityId()
    val aggregatedValue = integer("aggregated_value")
    val eventName = text("event_name")
    val orgId = text("org_id")
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }

    init {
        index("billing_meter_backups_stripe_customer_id_meter_id_start_tim_idx", false, stripeCustomerId, meterId, startTime, endTime)
        uniqueIndex("billing_meter_backups_stripe_customer_id_meter_id_start_tim_key", stripeCustomerId, meterId, startTime, endTime)
    }
}
