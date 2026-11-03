package umami.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.core.java.javaUUID
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object WebsiteEventTable : IdTable<UUID>("website_event") {
    override val id = javaUUID("event_id").entityId()
    val websiteId = javaUUID("website_id")
    val sessionId = javaUUID("session_id")
    val visitId = javaUUID("visit_id")
    val createdAt =
        pgTimestamptz("created_at", 6).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val urlPath = varchar("url_path", 500)
    val urlQuery = varchar("url_query", 500).nullable()
    val utmSource = varchar("utm_source", 255).nullable()
    val utmMedium = varchar("utm_medium", 255).nullable()
    val utmCampaign = varchar("utm_campaign", 255).nullable()
    val utmContent = varchar("utm_content", 255).nullable()
    val utmTerm = varchar("utm_term", 255).nullable()
    val referrerPath = varchar("referrer_path", 500).nullable()
    val referrerQuery = varchar("referrer_query", 500).nullable()
    val referrerDomain = varchar("referrer_domain", 500).nullable()
    val pageTitle = varchar("page_title", 500).nullable()
    val gclid = varchar("gclid", 255).nullable()
    val fbclid = varchar("fbclid", 255).nullable()
    val msclkid = varchar("msclkid", 255).nullable()
    val ttclid = varchar("ttclid", 255).nullable()
    val lifatid = varchar("li_fat_id", 255).nullable()
    val twclid = varchar("twclid", 255).nullable()
    val eventType = integer("event_type").default(1)
    val eventName = varchar("event_name", 50).nullable()
    val tag = varchar("tag", 50).nullable()
    val hostname = varchar("hostname", 100).nullable()
    val lcp = decimal("lcp", 10, 1).nullable()
    val inp = decimal("inp", 10, 1).nullable()
    val cls = decimal("cls", 10, 4).nullable()
    val fcp = decimal("fcp", 10, 1).nullable()
    val ttfb = decimal("ttfb", 10, 1).nullable()
    override val primaryKey = PrimaryKey(id, name = "website_event_pkey")

    init {
        index("website_event_created_at_idx", false, createdAt)
        index("website_event_session_id_idx", false, sessionId)
        index("website_event_visit_id_idx", false, visitId)
        index("website_event_website_id_idx", false, websiteId)
        index("website_event_website_id_created_at_idx", false, websiteId, createdAt)
        index("website_event_website_id_created_at_url_path_idx", false, websiteId, createdAt, urlPath)
        index("website_event_website_id_created_at_url_query_idx", false, websiteId, createdAt, urlQuery)
        index("website_event_website_id_created_at_referrer_domain_idx", false, websiteId, createdAt, referrerDomain)
        index("website_event_website_id_created_at_page_title_idx", false, websiteId, createdAt, pageTitle)
        index("website_event_website_id_created_at_event_name_idx", false, websiteId, createdAt, eventName)
        index("website_event_website_id_created_at_tag_idx", false, websiteId, createdAt, tag)
        index("website_event_website_id_session_id_created_at_idx", false, websiteId, sessionId, createdAt)
        index("website_event_website_id_visit_id_created_at_idx", false, websiteId, visitId, createdAt)
        index("website_event_website_id_created_at_hostname_idx", false, websiteId, createdAt, hostname)
    }
}
