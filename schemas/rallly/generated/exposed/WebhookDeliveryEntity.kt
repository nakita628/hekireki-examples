package rallly.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class WebhookDeliveryEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, WebhookDeliveryEntity>(WebhookDeliveryTable)

    var webhookId by WebhookDeliveryTable.webhookId
    var activityId by WebhookDeliveryTable.activityId
    var eventType by WebhookDeliveryTable.eventType
    var payload by WebhookDeliveryTable.payload
    var status by WebhookDeliveryTable.status
    var attempts by WebhookDeliveryTable.attempts
    var nextAttemptAt by WebhookDeliveryTable.nextAttemptAt
    var lastResponseStatus by WebhookDeliveryTable.lastResponseStatus
    var lastError by WebhookDeliveryTable.lastError
    var createdAt by WebhookDeliveryTable.createdAt
    var updatedAt by WebhookDeliveryTable.updatedAt
    var webhook by SpaceWebhookEntity referencedOn WebhookDeliveryTable.webhookId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(WebhookDeliveryTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
