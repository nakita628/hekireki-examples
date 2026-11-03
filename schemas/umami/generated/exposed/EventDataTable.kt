package umami.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.core.java.javaUUID
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object EventDataTable : IdTable<UUID>("event_data") {
    override val id = javaUUID("event_data_id").entityId()
    val websiteId = javaUUID("website_id")
    val websiteEventId = javaUUID("website_event_id")
    val dataKey = varchar("data_key", 500)
    val stringValue = varchar("string_value", 500).nullable()
    val numberValue = decimal("number_value", 19, 4).nullable()
    val dateValue = pgTimestamptz("date_value", 6).nullable()
    val dataType = integer("data_type")
    val createdAt =
        pgTimestamptz("created_at", 6).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "event_data_pkey")

    init {
        index("event_data_created_at_idx", false, createdAt)
        index("event_data_website_id_idx", false, websiteId)
        index("event_data_website_event_id_idx", false, websiteEventId)
        index("event_data_website_id_created_at_idx", false, websiteId, createdAt)
        index("event_data_website_id_created_at_data_key_idx", false, websiteId, createdAt, dataKey)
    }
}
