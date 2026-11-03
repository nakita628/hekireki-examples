package triggerdev.models

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

    var friendlyId by WebhookEndpointTable.friendlyId
    var opaqueId by WebhookEndpointTable.opaqueId
    var organizationId by WebhookEndpointTable.organizationId
    var projectId by WebhookEndpointTable.projectId
    var runtimeEnvironmentId by WebhookEndpointTable.runtimeEnvironmentId
    var environmentType by WebhookEndpointTable.environmentType
    var endpointTenantId by WebhookEndpointTable.endpointTenantId
    var endpointExternalRef by WebhookEndpointTable.endpointExternalRef
    var source by WebhookEndpointTable.source1
    var declaredId by WebhookEndpointTable.declaredId
    var routingTargets by WebhookEndpointTable.routingTargets
    var verifierArtifact by WebhookEndpointTable.verifierArtifact
    var metadata by WebhookEndpointTable.metadata
    var secretProvisioning by WebhookEndpointTable.secretProvisioning
    var setupPrompt by WebhookEndpointTable.setupPrompt
    var signingSecretKey by WebhookEndpointTable.signingSecretKey
    var status by WebhookEndpointTable.status
    var manuallyDeactivatedAt by WebhookEndpointTable.manuallyDeactivatedAt
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
