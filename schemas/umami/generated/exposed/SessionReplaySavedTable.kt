package umami.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.core.java.javaUUID
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object SessionReplaySavedTable : IdTable<UUID>("session_replay_saved") {
    override val id = javaUUID("saved_replay_id").entityId()
    val name = varchar("name", 100)
    val websiteId = javaUUID("website_id")
    val visitId = javaUUID("visit_id")
    val createdAt =
        pgTimestamptz("created_at", 6).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamptz("updated_at", 6).nullable().clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "session_replay_saved_pkey")

    init {
        index("session_replay_saved_website_id_idx", false, websiteId)
        index("session_replay_saved_visit_id_idx", false, visitId)
        index("session_replay_saved_website_id_created_at_idx", false, websiteId, createdAt)
        uniqueIndex("session_replay_saved_website_id_visit_id_key", websiteId, visitId)
    }
}
