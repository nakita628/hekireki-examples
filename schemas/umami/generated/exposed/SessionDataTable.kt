package umami.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.core.java.javaUUID
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object SessionDataTable : IdTable<UUID>("session_data") {
    override val id = javaUUID("session_data_id").entityId()
    val websiteId = javaUUID("website_id")
    val sessionId = javaUUID("session_id")
    val dataKey = varchar("data_key", 500)
    val stringValue = varchar("string_value", 500).nullable()
    val numberValue = decimal("number_value", 19, 4).nullable()
    val dateValue = pgTimestamptz("date_value", 6).nullable()
    val dataType = integer("data_type")
    val distinctId = varchar("distinct_id", 50).nullable()
    val createdAt =
        pgTimestamptz("created_at", 6).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "session_data_pkey")

    init {
        index("session_data_created_at_idx", false, createdAt)
        index("session_data_website_id_idx", false, websiteId)
        index("session_data_session_id_idx", false, sessionId)
        index("session_data_session_id_created_at_idx", false, sessionId, createdAt)
        index("session_data_website_id_created_at_data_key_idx", false, websiteId, createdAt, dataKey)
        uniqueIndex("session_data_session_id_data_key_key", sessionId, dataKey)
    }
}
