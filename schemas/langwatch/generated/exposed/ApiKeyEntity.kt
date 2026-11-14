package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class ApiKeyEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ApiKeyEntity>(ApiKeyTable)

    var name by ApiKeyTable.name
    var description by ApiKeyTable.description
    var lookupId by ApiKeyTable.lookupId
    var hashedSecret by ApiKeyTable.hashedSecret
    var permissionMode by ApiKeyTable.permissionMode
    var userId by ApiKeyTable.userId
    var createdByUserId by ApiKeyTable.createdByUserId
    var organizationId by ApiKeyTable.organizationId
    var expiresAt by ApiKeyTable.expiresAt
    var lastUsedAt by ApiKeyTable.lastUsedAt
    var revokedAt by ApiKeyTable.revokedAt
    var revocationCause by ApiKeyTable.revocationCause
    var parentApiKeyId by ApiKeyTable.parentApiKeyId
    var ingestSourceType by ApiKeyTable.ingestSourceType
    var ingestionTemplateId by ApiKeyTable.ingestionTemplateId
    var createdByDeviceLabel by ApiKeyTable.createdByDeviceLabel
    var isSystemManaged by ApiKeyTable.isSystemManaged
    var createdByBrowserSessionId by ApiKeyTable.createdByBrowserSessionId
    var createdAt by ApiKeyTable.createdAt
    var updatedAt by ApiKeyTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ApiKeyTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
