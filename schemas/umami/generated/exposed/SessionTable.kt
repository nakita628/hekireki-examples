package umami.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.core.java.javaUUID
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object SessionTable : IdTable<UUID>("session") {
    override val id = javaUUID("session_id").entityId()
    val websiteId = javaUUID("website_id")
    val browser = varchar("browser", 20).nullable()
    val os = varchar("os", 20).nullable()
    val device = varchar("device", 20).nullable()
    val screen = varchar("screen", 11).nullable()
    val language = varchar("language", 35).nullable()
    val country = char("country", 2).nullable()
    val region = varchar("region", 20).nullable()
    val city = varchar("city", 50).nullable()
    val distinctId = varchar("distinct_id", 50).nullable()
    val createdAt =
        pgTimestamptz("created_at", 6).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "session_pkey")

    init {
        index("session_created_at_idx", false, createdAt)
        index("session_website_id_idx", false, websiteId)
        index("session_website_id_created_at_idx", false, websiteId, createdAt)
        index("session_website_id_created_at_browser_idx", false, websiteId, createdAt, browser)
        index("session_website_id_created_at_os_idx", false, websiteId, createdAt, os)
        index("session_website_id_created_at_device_idx", false, websiteId, createdAt, device)
        index("session_website_id_created_at_screen_idx", false, websiteId, createdAt, screen)
        index("session_website_id_created_at_language_idx", false, websiteId, createdAt, language)
        index("session_website_id_created_at_country_idx", false, websiteId, createdAt, country)
        index("session_website_id_created_at_region_idx", false, websiteId, createdAt, region)
        index("session_website_id_created_at_city_idx", false, websiteId, createdAt, city)
    }
}
