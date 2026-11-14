package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class WebhookEndpointEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, WebhookEndpointEntity>(WebhookEndpointTable)

    var organizationId by WebhookEndpointTable.organizationId
    var destinationKind by WebhookEndpointTable.destinationKind
    var url by WebhookEndpointTable.url
    var sqsQueueUrl by WebhookEndpointTable.sqsQueueUrl
    var sqsRoleArn by WebhookEndpointTable.sqsRoleArn
    var sqsExternalId by WebhookEndpointTable.sqsExternalId
    var sqsAccessKeyId by WebhookEndpointTable.sqsAccessKeyId
    var sqsSecretAccessKeyEncrypted by WebhookEndpointTable.sqsSecretAccessKeyEncrypted
    var secretEncrypted by WebhookEndpointTable.secretEncrypted
    var signatureScheme by WebhookEndpointTable.signatureScheme
    var idempotencyKey by WebhookEndpointTable.idempotencyKey
    var previousSecretEncrypted by WebhookEndpointTable.previousSecretEncrypted
    var previousSecretExpiresAt by WebhookEndpointTable.previousSecretExpiresAt
    var enabledEvents by WebhookEndpointTable.enabledEvents
    var status by WebhookEndpointTable.status
    var disabledReason by WebhookEndpointTable.disabledReason
    var disabledAt by WebhookEndpointTable.disabledAt
    var failingSince by WebhookEndpointTable.failingSince
    var maxBatchSize by WebhookEndpointTable.maxBatchSize
    var maxBatchDelayMs by WebhookEndpointTable.maxBatchDelayMs
    var maxInFlight by WebhookEndpointTable.maxInFlight
    var allowSelfSignedCertificate by WebhookEndpointTable.allowSelfSignedCertificate
    var lastSuccessAt by WebhookEndpointTable.lastSuccessAt
    var lastFailureAt by WebhookEndpointTable.lastFailureAt
    var archivedAt by WebhookEndpointTable.archivedAt
    var createdAt by WebhookEndpointTable.createdAt
    var updatedAt by WebhookEndpointTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(WebhookEndpointTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
