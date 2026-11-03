package umami.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass
import java.util.UUID

class ApiKeyEntity(
    id: EntityID<UUID>,
) : Entity<UUID>(id) {
    companion object : EntityClass<UUID, ApiKeyEntity>(ApiKeyTable)

    var userId by ApiKeyTable.userId
    var name by ApiKeyTable.name
    var keyHash by ApiKeyTable.keyHash
    var keyPrefix by ApiKeyTable.keyPrefix
    var lastUsedAt by ApiKeyTable.lastUsedAt
    var createdAt by ApiKeyTable.createdAt
}
