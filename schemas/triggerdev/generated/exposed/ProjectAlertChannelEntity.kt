package triggerdev.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class ProjectAlertChannelEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ProjectAlertChannelEntity>(ProjectAlertChannelTable)

    var friendlyId by ProjectAlertChannelTable.friendlyId
    var deduplicationKey by ProjectAlertChannelTable.deduplicationKey
    var userProvidedDeduplicationKey by ProjectAlertChannelTable.userProvidedDeduplicationKey
    var integration by OrganizationIntegrationEntity optionalReferencedOn ProjectAlertChannelTable.integrationId
    var integrationId by ProjectAlertChannelTable.integrationId
    var enabled by ProjectAlertChannelTable.enabled
    var type by ProjectAlertChannelTable.type
    var name by ProjectAlertChannelTable.name
    var properties by ProjectAlertChannelTable.properties
    var alertTypes by ProjectAlertChannelTable.alertTypes
    var environmentTypes by ProjectAlertChannelTable.environmentTypes
    var errorAlertConfig by ProjectAlertChannelTable.errorAlertConfig
    var project by ProjectEntity referencedOn ProjectAlertChannelTable.projectId
    var projectId by ProjectAlertChannelTable.projectId
    var createdAt by ProjectAlertChannelTable.createdAt
    var updatedAt by ProjectAlertChannelTable.updatedAt
    val alerts by ProjectAlertEntity referrersOn ProjectAlertTable.channelId
    val alertStorages by ProjectAlertStorageEntity referrersOn ProjectAlertStorageTable.alertChannelId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ProjectAlertChannelTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
