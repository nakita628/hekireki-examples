package langfuse.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class PosthogIntegrationEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, PosthogIntegrationEntity>(PosthogIntegrationTable)

    val project by ProjectEntity referencedOn PosthogIntegrationTable.id
    var encryptedPosthogApiKey by PosthogIntegrationTable.encryptedPosthogApiKey
    var posthogHostName by PosthogIntegrationTable.posthogHostName
    var lastSyncAt by PosthogIntegrationTable.lastSyncAt
    var enabled by PosthogIntegrationTable.enabled
    var createdAt by PosthogIntegrationTable.createdAt
    var exportSource by PosthogIntegrationTable.exportSource
    var backfill by PosthogIntegrationTable.backfill
    var lastError by PosthogIntegrationTable.lastError
    var lastErrorAt by PosthogIntegrationTable.lastErrorAt
}
