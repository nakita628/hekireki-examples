package triggerdev.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import java.time.Instant
import java.time.temporal.ChronoUnit

class WebhookDeliveryEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<WebhookDeliveryEntity>(WebhookDeliveryTable)

    val id1 by WebhookDeliveryTable.id1
    var friendlyId by WebhookDeliveryTable.friendlyId
    var webhookEndpointId by WebhookDeliveryTable.webhookEndpointId
    var organizationId by WebhookDeliveryTable.organizationId
    var projectId by WebhookDeliveryTable.projectId
    var runtimeEnvironmentId by WebhookDeliveryTable.runtimeEnvironmentId
    var environmentType by WebhookDeliveryTable.environmentType
    var externalDeliveryId by WebhookDeliveryTable.externalDeliveryId
    var idempotencyKey by WebhookDeliveryTable.idempotencyKey
    var runId by WebhookDeliveryTable.runId
    var status by WebhookDeliveryTable.status
    var isTest by WebhookDeliveryTable.isTest
    var parsedEvent by WebhookDeliveryTable.parsedEvent
    var headers by WebhookDeliveryTable.headers
    var rawBodyHash by WebhookDeliveryTable.rawBodyHash
    var errorMessage by WebhookDeliveryTable.errorMessage
    var filterReason by WebhookDeliveryTable.filterReason
    var targets by WebhookDeliveryTable.targets
    val createdAt by WebhookDeliveryTable.createdAt
    var updatedAt by WebhookDeliveryTable.updatedAt
    var processedAt by WebhookDeliveryTable.processedAt
    val retentionDays by WebhookDeliveryTable.retentionDays

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(WebhookDeliveryTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
