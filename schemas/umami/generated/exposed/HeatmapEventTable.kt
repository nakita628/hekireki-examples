package umami.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.core.java.javaUUID
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object HeatmapEventTable : IdTable<UUID>("heatmap_event") {
    override val id = javaUUID("heatmap_event_id").entityId()
    val websiteId = javaUUID("website_id")
    val sessionId = javaUUID("session_id")
    val visitId = javaUUID("visit_id")
    val urlPath = varchar("url_path", 500)
    val eventType = integer("event_type")
    val x = integer("x").nullable()
    val y = integer("y").nullable()
    val pageX = integer("page_x").nullable()
    val pageY = integer("page_y").nullable()
    val pageW = integer("page_w").nullable()
    val viewportW = integer("viewport_w").nullable()
    val viewportH = integer("viewport_h").nullable()
    val pageH = integer("page_h").nullable()
    val scrollPct = integer("scroll_pct").nullable()
    val createdAt = pgTimestamptz("created_at", 6).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "heatmap_event_pkey")

    init {
        index("heatmap_event_website_id_idx", false, websiteId)
        index("heatmap_event_visit_id_idx", false, visitId)
        index("heatmap_event_website_id_created_at_idx", false, websiteId, createdAt)
        index("heatmap_event_website_id_url_path_event_type_created_at_idx", false, websiteId, urlPath, eventType, createdAt)
    }
}
