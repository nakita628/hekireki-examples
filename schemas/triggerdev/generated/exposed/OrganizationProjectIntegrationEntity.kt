package triggerdev.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class OrganizationProjectIntegrationEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, OrganizationProjectIntegrationEntity>(OrganizationProjectIntegrationTable)

    var organizationIntegration by OrganizationIntegrationEntity referencedOn OrganizationProjectIntegrationTable.organizationIntegrationId
    var organizationIntegrationId by OrganizationProjectIntegrationTable.organizationIntegrationId
    var project by ProjectEntity referencedOn OrganizationProjectIntegrationTable.projectId
    var projectId by OrganizationProjectIntegrationTable.projectId
    var externalEntityId by OrganizationProjectIntegrationTable.externalEntityId
    var integrationData by OrganizationProjectIntegrationTable.integrationData
    var installedBy by OrganizationProjectIntegrationTable.installedBy
    var createdAt by OrganizationProjectIntegrationTable.createdAt
    var updatedAt by OrganizationProjectIntegrationTable.updatedAt
    var deletedAt by OrganizationProjectIntegrationTable.deletedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(OrganizationProjectIntegrationTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
