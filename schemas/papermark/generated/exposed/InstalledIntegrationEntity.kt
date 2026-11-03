package papermark.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class InstalledIntegrationEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, InstalledIntegrationEntity>(InstalledIntegrationTable)

    var credentials by InstalledIntegrationTable.credentials
    var configuration by InstalledIntegrationTable.configuration
    var enabled by InstalledIntegrationTable.enabled
    var integrationId by InstalledIntegrationTable.integrationId
    var integration by IntegrationEntity referencedOn InstalledIntegrationTable.integrationId
    var userId by InstalledIntegrationTable.userId
    var user by UserEntity optionalReferencedOn InstalledIntegrationTable.userId
    var teamId by InstalledIntegrationTable.teamId
    var team by TeamEntity referencedOn InstalledIntegrationTable.teamId
    var createdAt by InstalledIntegrationTable.createdAt
    var updatedAt by InstalledIntegrationTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(InstalledIntegrationTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
