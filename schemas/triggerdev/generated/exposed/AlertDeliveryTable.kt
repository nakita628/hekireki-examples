package triggerdev.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object AlertDeliveryTable : CompositeIdTable("\"AlertDelivery\"") {
    val id1 = text("id").entityId()
    val alertEventId = text("alertEventId")
    val eventAt = pgTimestamptz("eventAt", 6).entityId()
    val notifierId = text("notifierId")
    val notifier = jsonb<String>("notifier", { it }, { it })
    val status = text("status").default("pending")
    val claimId = text("claimId").nullable()
    val claimedAt = pgTimestamptz("claimedAt", 6).nullable()
    val deliveredAt = pgTimestamptz("deliveredAt", 6).nullable()
    val failureReason = text("failureReason").nullable()
    val providerMetadata = jsonb<String>("providerMetadata", { it }, { it }).nullable()
    val attempts = integer("attempts").default(0)
    val firstAttemptAt = pgTimestamptz("firstAttemptAt", 6).nullable()
    val createdAt = pgTimestamptz("createdAt", 6).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id1, eventAt, name = "\"AlertDelivery_pkey\"")

    init {
        index("\"AlertDelivery_status_createdAt_idx\"", false, status, createdAt)
        index("\"AlertDelivery_status_claimedAt_idx\"", false, status, claimedAt)
        uniqueIndex("\"AlertDelivery_alertEventId_notifierId_eventAt_key\"", alertEventId, notifierId, eventAt)
    }
}
