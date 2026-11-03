package umami.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable
import org.jetbrains.exposed.v1.core.java.javaUUID
import java.time.Instant
import java.time.temporal.ChronoUnit

object SessionLinkTable : CompositeIdTable("session_link") {
    val websiteId = javaUUID("website_id").entityId()
    val sessionId = javaUUID("session_id").entityId()
    val distinctId = varchar("distinct_id", 50).entityId()
    val createdAt =
        pgTimestamptz("created_at", 6).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(websiteId, distinctId, sessionId, name = "session_link_pkey")

    init {
        index("session_link_website_id_session_id_idx", false, websiteId, sessionId)
    }
}
