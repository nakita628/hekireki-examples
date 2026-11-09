package jitsu.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable

object RateLimitCounterTable : CompositeIdTable("\"RateLimitCounter\"") {
    val bucketKey = text("bucketKey").entityId()
    val windowStart = pgTimestamptz("windowStart", 3).entityId()
    val count = integer("count").default(0)
    val expiresAt = pgTimestamptz("expiresAt", 3)
    override val primaryKey = PrimaryKey(bucketKey, windowStart, name = "\"RateLimitCounter_pkey\"")

    init {
        index("\"RateLimitCounter_expiresAt_idx\"", false, expiresAt)
    }
}
