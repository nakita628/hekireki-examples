package documenso.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass

class RateLimitEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<RateLimitEntity>(RateLimitTable)

    val key by RateLimitTable.key
    val action by RateLimitTable.action
    val bucket by RateLimitTable.bucket
    var count by RateLimitTable.count
    var createdAt by RateLimitTable.createdAt
}
