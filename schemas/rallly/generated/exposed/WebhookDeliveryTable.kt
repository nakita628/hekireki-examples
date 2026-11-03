package rallly.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object WebhookDeliveryTable : IdTable<String>("webhook_deliveries") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val webhookId =
        reference(
            "webhook_id",
            SpaceWebhookTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "webhook_deliveries_webhook_id_fkey",
        )
    val activityId = text("activity_id")
    val eventType = text("event_type")
    val payload = jsonb<String>("payload", { it }, { it })
    val status =
        pgEnum("status", "\"webhook_delivery_status\"", WebhookDeliveryStatus.entries, WebhookDeliveryStatus::dbName).default(
            WebhookDeliveryStatus.PENDING,
        )
    val attempts = integer("attempts").default(0)
    val nextAttemptAt =
        pgTimestamp("next_attempt_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val lastResponseStatus = integer("last_response_status").nullable()
    val lastError = text("last_error").nullable()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "webhook_deliveries_pkey")

    init {
        index("webhook_deliveries_status_next_attempt_at_idx", false, status, nextAttemptAt)
        uniqueIndex("webhook_deliveries_webhook_id_activity_id_key", webhookId, activityId)
    }
}
