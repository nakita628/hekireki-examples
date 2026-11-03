package umami.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.core.java.javaUUID
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object PixelTable : IdTable<UUID>("pixel") {
    override val id = javaUUID("pixel_id").entityId()
    val name = varchar("name", 100)
    val slug = varchar("slug", 100).uniqueIndex("pixel_slug_key")
    val userId = javaUUID("user_id").nullable()
    val teamId = javaUUID("team_id").nullable()
    val createdAt =
        pgTimestamptz("created_at", 6).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamptz("updated_at", 6).nullable().clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val deletedAt = pgTimestamptz("deleted_at", 6).nullable()
    override val primaryKey = PrimaryKey(id, name = "pixel_pkey")

    init {
        index("pixel_slug_idx", false, slug)
        index("pixel_user_id_idx", false, userId)
        index("pixel_team_id_idx", false, teamId)
        index("pixel_created_at_idx", false, createdAt)
    }
}
