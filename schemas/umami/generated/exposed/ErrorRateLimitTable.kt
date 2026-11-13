package umami.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.core.java.javaUUID
import java.util.UUID

object ErrorRateLimitTable : IdTable<UUID>("error_rate_limit") {
    override val id = javaUUID("website_id").entityId()
    val bucket = pgTimestamptz("bucket", 6)
    val count = integer("count")
    override val primaryKey = PrimaryKey(id, name = "error_rate_limit_pkey")
}
