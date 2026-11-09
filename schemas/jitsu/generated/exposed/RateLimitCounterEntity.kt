package jitsu.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass

class RateLimitCounterEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<RateLimitCounterEntity>(RateLimitCounterTable)

    val bucketKey by RateLimitCounterTable.bucketKey
    val windowStart by RateLimitCounterTable.windowStart
    var count by RateLimitCounterTable.count
    var expiresAt by RateLimitCounterTable.expiresAt
}
