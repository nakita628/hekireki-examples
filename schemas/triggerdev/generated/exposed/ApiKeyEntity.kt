package triggerdev.models

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
    var keyHash by ApiKeyTable.keyHash
    var lastFour by ApiKeyTable.lastFour
    var description by ApiKeyTable.description
    var runtimeEnvironment by RuntimeEnvironmentEntity referencedOn ApiKeyTable.runtimeEnvironmentId
    var runtimeEnvironmentId by ApiKeyTable.runtimeEnvironmentId
    var createdBy by UserEntity optionalReferencedOn ApiKeyTable.createdByUserId
    var createdByUserId by ApiKeyTable.createdByUserId
    var presetId by ApiKeyTable.presetId
    var scopes by ApiKeyTable.scopes
    var allowedIpRanges by ApiKeyTable.allowedIpRanges
    var lastUsedAt by ApiKeyTable.lastUsedAt
    var lastUsedIp by ApiKeyTable.lastUsedIp
    var revokedAt by ApiKeyTable.revokedAt
    var expiresAt by ApiKeyTable.expiresAt
    var updatedAt by ApiKeyTable.updatedAt
    var createdAt by ApiKeyTable.createdAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ApiKeyTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
