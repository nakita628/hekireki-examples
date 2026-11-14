package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class ModelProviderEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ModelProviderEntity>(ModelProviderTable)

    var name by ModelProviderTable.name
    var provider by ModelProviderTable.provider
    var routingHandle by ModelProviderTable.routingHandle
    var enabled by ModelProviderTable.enabled
    var customKeys by ModelProviderTable.customKeys
    var extraHeaders by ModelProviderTable.extraHeaders
    var customModels by ModelProviderTable.customModels
    var customEmbeddingsModels by ModelProviderTable.customEmbeddingsModels
    var deploymentMapping by ModelProviderTable.deploymentMapping
    var rateLimitRpm by ModelProviderTable.rateLimitRpm
    var rateLimitTpm by ModelProviderTable.rateLimitTpm
    var rateLimitRpd by ModelProviderTable.rateLimitRpd
    var rotationPolicy by ModelProviderTable.rotationPolicy
    var providerConfig by ModelProviderTable.providerConfig
    var fallbackPriorityGlobal by ModelProviderTable.fallbackPriorityGlobal
    var langySkipPermissionsModels by ModelProviderTable.langySkipPermissionsModels
    var healthStatus by ModelProviderTable.healthStatus
    var circuitOpenedAt by ModelProviderTable.circuitOpenedAt
    var lastHealthCheckAt by ModelProviderTable.lastHealthCheckAt
    var disabledAt by ModelProviderTable.disabledAt
    var createdAt by ModelProviderTable.createdAt
    var updatedAt by ModelProviderTable.updatedAt
    var organizationId by ModelProviderTable.organizationId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ModelProviderTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
