package opensaas.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object ProcessedWebhookEventTable : IdTable<String>("\"ProcessedWebhookEvent\"") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val paymentProcessorEventId = text("paymentProcessorEventId").uniqueIndex("\"ProcessedWebhookEvent_paymentProcessorEventId_key\"")
    val eventType = text("eventType")
    override val primaryKey = PrimaryKey(id, name = "\"ProcessedWebhookEvent_pkey\"")
}
