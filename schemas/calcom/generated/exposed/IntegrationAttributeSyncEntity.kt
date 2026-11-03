package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class IntegrationAttributeSyncEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, IntegrationAttributeSyncEntity>(IntegrationAttributeSyncTable)

    var organizationId by IntegrationAttributeSyncTable.organizationId
    var organization by TeamEntity referencedOn IntegrationAttributeSyncTable.organizationId
    var name by IntegrationAttributeSyncTable.name
    var integration by IntegrationAttributeSyncTable.integration
    var credentialId by IntegrationAttributeSyncTable.credentialId
    var credential by CredentialEntity optionalReferencedOn IntegrationAttributeSyncTable.credentialId
    var enabled by IntegrationAttributeSyncTable.enabled
    val attributeSyncRule by AttributeSyncRuleEntity optionalBackReferencedOn AttributeSyncRuleTable.integrationAttributeSyncId
    val syncFieldMappings by AttributeSyncFieldMappingEntity referrersOn AttributeSyncFieldMappingTable.integrationAttributeSyncId
    var createdAt by IntegrationAttributeSyncTable.createdAt
    var updatedAt by IntegrationAttributeSyncTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(IntegrationAttributeSyncTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
