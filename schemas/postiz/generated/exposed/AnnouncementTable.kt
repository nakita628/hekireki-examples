package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object AnnouncementTable : IdTable<String>("\"Announcement\"") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val title = text("title")
    val description = text("description")
    val color =
        pgEnum("color", "\"AnnouncementColor\"", AnnouncementColor.entries, AnnouncementColor::dbName).default(AnnouncementColor.INFO)
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"Announcement_pkey\"")
}
