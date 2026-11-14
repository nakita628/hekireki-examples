package langwatch.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object BillingMeterCheckpointTable : IdTable<String>("\"BillingMeterCheckpoint\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val organizationId = text("organizationId")
    val billingMonth = text("billingMonth")
    val meter = text("meter").default("langwatch_billable_events")
    val lastReportedTotal = integer("lastReportedTotal").default(0)
    val pendingReportedTotal = integer("pendingReportedTotal").nullable()
    val consecutiveFailures = integer("consecutiveFailures").default(0)
    val lastCountedEventId = text("lastCountedEventId").nullable()
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"BillingMeterCheckpoint_pkey\"")

    init {
        uniqueIndex("\"BillingMeterCheckpoint_organizationId_billingMonth_meter_key\"", organizationId, billingMonth, meter)
    }
}
