package workoutcool.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object RevenueCatWebhookEventTable : IdTable<String>("revenuecat_webhook_events") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val eventType = text("eventType")
    val eventTimestamp = pgTimestamp("eventTimestamp", 3)
    val appUserId = text("appUserId")
    val environment = text("environment")
    val productId = text("productId").nullable()
    val transactionId = text("transactionId").nullable()
    val originalTransactionId = text("originalTransactionId").nullable()
    val entitlementIds = text("entitlementIds").nullable()
    val processed = bool("processed").default(false)
    val processingError = text("processingError").nullable()
    val retryCount = integer("retryCount").default(0)
    val rawEventData = jsonb<String>("rawEventData", { it }, { it })
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "revenuecat_webhook_events_pkey")

    init {
        index("\"revenuecat_webhook_events_appUserId_idx\"", false, appUserId)
        index("\"revenuecat_webhook_events_eventType_idx\"", false, eventType)
        index("revenuecat_webhook_events_processed_idx", false, processed)
        index("\"revenuecat_webhook_events_eventTimestamp_idx\"", false, eventTimestamp)
    }
}
