package documenso.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object RateLimitTable : CompositeIdTable("\"RateLimit\"") {
    val key = text("key").entityId()
    val action = text("action").entityId()
    val bucket = pgTimestamp("bucket", 3).entityId()
    val count = integer("count").default(1)
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(key, action, bucket, name = "\"RateLimit_pkey\"")

    init {
        index("\"RateLimit_createdAt_idx\"", false, createdAt)
    }
}
