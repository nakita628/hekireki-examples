package triggerdev.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class OrganizationIntegrationEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, OrganizationIntegrationEntity>(OrganizationIntegrationTable)

    var friendlyId by OrganizationIntegrationTable.friendlyId
    var service by OrganizationIntegrationTable.service
    var externalOrganizationId by OrganizationIntegrationTable.externalOrganizationId
    var integrationData by OrganizationIntegrationTable.integrationData
    var tokenReference by SecretReferenceEntity referencedOn OrganizationIntegrationTable.tokenReferenceId
    var tokenReferenceId by OrganizationIntegrationTable.tokenReferenceId
    var organization by OrganizationEntity referencedOn OrganizationIntegrationTable.organizationId
    var organizationId by OrganizationIntegrationTable.organizationId
    var createdAt by OrganizationIntegrationTable.createdAt
    var updatedAt by OrganizationIntegrationTable.updatedAt
    var deletedAt by OrganizationIntegrationTable.deletedAt
    val alertChannels by ProjectAlertChannelEntity optionalReferrersOn ProjectAlertChannelTable.integrationId
    val organizationProjectIntegration by OrganizationProjectIntegrationEntity referrersOn
        OrganizationProjectIntegrationTable.organizationIntegrationId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(OrganizationIntegrationTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
