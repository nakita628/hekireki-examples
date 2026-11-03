package rallly.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class SpaceApiKeyEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, SpaceApiKeyEntity>(SpaceApiKeyTable)

    var spaceId by SpaceApiKeyTable.spaceId
    var name by SpaceApiKeyTable.name
    var prefix by SpaceApiKeyTable.prefix
    var hashedKey by SpaceApiKeyTable.hashedKey
    var lastUsedAt by SpaceApiKeyTable.lastUsedAt
    var expiresAt by SpaceApiKeyTable.expiresAt
    var revokedAt by SpaceApiKeyTable.revokedAt
    var createdAt by SpaceApiKeyTable.createdAt
    var updatedAt by SpaceApiKeyTable.updatedAt
    var space by SpaceEntity referencedOn SpaceApiKeyTable.spaceId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(SpaceApiKeyTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
