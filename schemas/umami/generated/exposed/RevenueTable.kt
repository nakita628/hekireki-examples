package umami.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.core.java.javaUUID
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object RevenueTable : IdTable<UUID>("revenue") {
    override val id = javaUUID("revenue_id").entityId()
    val websiteId = javaUUID("website_id")
    val sessionId = javaUUID("session_id")
    val eventId = javaUUID("event_id")
    val eventName = varchar("event_name", 50)
    val currency = varchar("currency", 10)
    val revenue = decimal("revenue", 19, 4).nullable()
    val createdAt =
        pgTimestamptz("created_at", 6).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "revenue_pkey")

    init {
        index("revenue_website_id_idx", false, websiteId)
        index("revenue_session_id_idx", false, sessionId)
        index("revenue_website_id_created_at_idx", false, websiteId, createdAt)
        index("revenue_website_id_session_id_created_at_idx", false, websiteId, sessionId, createdAt)
    }
}
