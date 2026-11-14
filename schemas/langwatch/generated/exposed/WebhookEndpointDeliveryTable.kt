package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object WebhookEndpointDeliveryTable : IdTable<String>("\"WebhookEndpointDelivery\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val channel =
        pgEnum("channel", "\"WebhookDeliveryChannel\"", WebhookDeliveryChannel.entries, WebhookDeliveryChannel::dbName).default(
            WebhookDeliveryChannel.PLATFORM,
        )
    val organizationId = text("organizationId").nullable()
    val endpointId = text("endpointId").nullable()
    val projectId = text("projectId").nullable()
    val triggerId = text("triggerId").nullable()
    val dispatchId = text("dispatchId")
    val attempt = integer("attempt").nullable()
    val eventCount = integer("eventCount").nullable()
    val responseStatus = integer("responseStatus").nullable()
    val latencyMs = integer("latencyMs").nullable()
    val error = text("error").nullable()
    val response = jsonb<String>("response", { it }, { it }).nullable()
    val outcome = pgEnum("outcome", "\"WebhookDeliveryOutcome\"", WebhookDeliveryOutcome.entries, WebhookDeliveryOutcome::dbName)
    val firedAt = pgTimestamp("firedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"WebhookEndpointDelivery_pkey\"")

    init {
        index("\"WebhookEndpointDelivery_endpointId_firedAt_idx\"", false, endpointId, firedAt)
        index("\"WebhookEndpointDelivery_organizationId_firedAt_idx\"", false, organizationId, firedAt)
        index("\"WebhookEndpointDelivery_firedAt_idx\"", false, firedAt)
        index("\"WebhookEndpointDelivery_endpointId_dispatchId_idx\"", false, endpointId, dispatchId)
        index("\"WebhookEndpointDelivery_projectId_triggerId_firedAt_idx\"", false, projectId, triggerId, firedAt)
        index("\"WebhookEndpointDelivery_projectId_triggerId_dispatchId_idx\"", false, projectId, triggerId, dispatchId)
    }
}
