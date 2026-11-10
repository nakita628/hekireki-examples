package opensaas.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class ProcessedWebhookEventEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ProcessedWebhookEventEntity>(ProcessedWebhookEventTable)

    var createdAt by ProcessedWebhookEventTable.createdAt
    var paymentProcessorEventId by ProcessedWebhookEventTable.paymentProcessorEventId
    var eventType by ProcessedWebhookEventTable.eventType
}
