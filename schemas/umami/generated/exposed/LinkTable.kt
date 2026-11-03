package umami.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.core.java.javaUUID
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object LinkTable : IdTable<UUID>("link") {
    override val id = javaUUID("link_id").entityId()
    val name = varchar("name", 100)
    val url = varchar("url", 500)
    val slug = varchar("slug", 100).uniqueIndex("link_slug_key")
    val userId = javaUUID("user_id").nullable()
    val teamId = javaUUID("team_id").nullable()
    val createdAt =
        pgTimestamptz("created_at", 6).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamptz("updated_at", 6).nullable().clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val deletedAt = pgTimestamptz("deleted_at", 6).nullable()
    override val primaryKey = PrimaryKey(id, name = "link_pkey")

    init {
        index("link_slug_idx", false, slug)
        index("link_user_id_idx", false, userId)
        index("link_team_id_idx", false, teamId)
        index("link_created_at_idx", false, createdAt)
    }
}
