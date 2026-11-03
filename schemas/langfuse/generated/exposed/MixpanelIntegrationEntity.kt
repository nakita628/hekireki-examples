package langfuse.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class MixpanelIntegrationEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, MixpanelIntegrationEntity>(MixpanelIntegrationTable)

    val project by ProjectEntity referencedOn MixpanelIntegrationTable.id
    var encryptedMixpanelProjectToken by MixpanelIntegrationTable.encryptedMixpanelProjectToken
    var mixpanelRegion by MixpanelIntegrationTable.mixpanelRegion
    var lastSyncAt by MixpanelIntegrationTable.lastSyncAt
    var enabled by MixpanelIntegrationTable.enabled
    var createdAt by MixpanelIntegrationTable.createdAt
    var exportSource by MixpanelIntegrationTable.exportSource
    var backfill by MixpanelIntegrationTable.backfill
}
