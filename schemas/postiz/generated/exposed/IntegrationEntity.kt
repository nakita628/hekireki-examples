package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class IntegrationEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, IntegrationEntity>(IntegrationTable)

    var internalId by IntegrationTable.internalId
    var organizationId by IntegrationTable.organizationId
    var name by IntegrationTable.name
    var customName by IntegrationTable.customName
    var picture by IntegrationTable.picture
    var providerIdentifier by IntegrationTable.providerIdentifier
    var type by IntegrationTable.type
    var token by IntegrationTable.token
    var disabled by IntegrationTable.disabled
    var tokenExpiration by IntegrationTable.tokenExpiration
    var refreshToken by IntegrationTable.refreshToken
    var profile by IntegrationTable.profile
    var deletedAt by IntegrationTable.deletedAt
    var createdAt by IntegrationTable.createdAt
    var updatedAt by IntegrationTable.updatedAt
    var inBetweenSteps by IntegrationTable.inBetweenSteps
    var refreshNeeded by IntegrationTable.refreshNeeded
    var postingTimes by IntegrationTable.postingTimes
    var customInstanceDetails by IntegrationTable.customInstanceDetails
    var customerId by IntegrationTable.customerId
    var rootInternalId by IntegrationTable.rootInternalId
    var additionalSettings by IntegrationTable.additionalSettings
    val exisingPlugData by ExisingPlugDataEntity referrersOn ExisingPlugDataTable.integrationId
    var customer by CustomerEntity optionalReferencedOn IntegrationTable.customerId
    var organization by OrganizationEntity referencedOn IntegrationTable.organizationId
    val webhooks by IntegrationsWebhooksEntity referrersOn IntegrationsWebhooksTable.integrationId
    val orderItems by OrderItemsEntity referrersOn OrderItemsTable.integrationId
    val plugs by PlugsEntity referrersOn PlugsTable.integrationId
    val posts by PostEntity referrersOn PostTable.integrationId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(IntegrationTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
