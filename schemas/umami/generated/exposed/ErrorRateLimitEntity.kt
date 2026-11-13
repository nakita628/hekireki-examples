package umami.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass
import java.util.UUID

class ErrorRateLimitEntity(
    id: EntityID<UUID>,
) : Entity<UUID>(id) {
    companion object : EntityClass<UUID, ErrorRateLimitEntity>(ErrorRateLimitTable)

    var bucket by ErrorRateLimitTable.bucket
    var count by ErrorRateLimitTable.count
}
