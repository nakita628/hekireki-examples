package umami.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable
import org.jetbrains.exposed.v1.core.java.javaUUID
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object ErrorEventTable : CompositeIdTable("error_event") {
    val websiteId = javaUUID("website_id").entityId()
    val id1 = javaUUID("event_id").entityId()
    val issueId = javaUUID("issue_id")
    val sessionId = javaUUID("session_id")
    val visitId = javaUUID("visit_id")
    val createdAt = pgTimestamptz("created_at", 6)
    val receivedAt = pgTimestamptz("received_at", 6).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val name = varchar("name", 200)
    val message = varchar("message", 2000)
    val stack = text("stack")
    val frames = jsonb<String>("frames", { it }, { it })
    val tags = jsonb<String>("tags", { it }, { it })
    val handled = bool("handled")
    val urlPath = varchar("url_path", 500)
    val release = varchar("release", 100)
    val environment = varchar("environment", 50)
    val browser = varchar("browser", 50)
    val os = varchar("os", 50)
    val device = varchar("device", 50)
    override val primaryKey = PrimaryKey(websiteId, id1, name = "error_event_pkey")

    init {
        index("error_event_website_id_created_at_idx", false, websiteId, createdAt)
        index("error_event_website_id_issue_id_created_at_idx", false, websiteId, issueId, createdAt)
    }
}
