package umami.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.core.java.javaUUID
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object SessionReplayTable : IdTable<UUID>("session_replay") {
    override val id = javaUUID("replay_id").entityId()
    val websiteId = javaUUID("website_id")
    val sessionId = javaUUID("session_id")
    val visitId = javaUUID("visit_id")
    val chunkIndex = integer("chunk_index")
    val events = binary("events")
    val eventCount = integer("event_count")
    val startedAt = pgTimestamptz("started_at", 6)
    val endedAt = pgTimestamptz("ended_at", 6)
    val createdAt =
        pgTimestamptz("created_at", 6).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "session_replay_pkey")

    init {
        index("session_replay_website_id_idx", false, websiteId)
        index("session_replay_session_id_idx", false, sessionId)
        index("session_replay_visit_id_idx", false, visitId)
        index("session_replay_website_id_session_id_idx", false, websiteId, sessionId)
        index("session_replay_website_id_visit_id_idx", false, websiteId, visitId)
        index("session_replay_website_id_created_at_idx", false, websiteId, createdAt)
        index("session_replay_session_id_chunk_index_idx", false, sessionId, chunkIndex)
    }
}
