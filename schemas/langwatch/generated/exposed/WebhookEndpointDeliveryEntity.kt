package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class WebhookEndpointDeliveryEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, WebhookEndpointDeliveryEntity>(WebhookEndpointDeliveryTable)

    var channel by WebhookEndpointDeliveryTable.channel
    var organizationId by WebhookEndpointDeliveryTable.organizationId
    var endpointId by WebhookEndpointDeliveryTable.endpointId
    var projectId by WebhookEndpointDeliveryTable.projectId
    var triggerId by WebhookEndpointDeliveryTable.triggerId
    var dispatchId by WebhookEndpointDeliveryTable.dispatchId
    var attempt by WebhookEndpointDeliveryTable.attempt
    var eventCount by WebhookEndpointDeliveryTable.eventCount
    var responseStatus by WebhookEndpointDeliveryTable.responseStatus
    var latencyMs by WebhookEndpointDeliveryTable.latencyMs
    var error by WebhookEndpointDeliveryTable.error
    var response by WebhookEndpointDeliveryTable.response
    var outcome by WebhookEndpointDeliveryTable.outcome
    var firedAt by WebhookEndpointDeliveryTable.firedAt
    var createdAt by WebhookEndpointDeliveryTable.createdAt
}
