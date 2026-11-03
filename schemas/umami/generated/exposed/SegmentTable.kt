package umami.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.core.java.javaUUID
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object SegmentTable : IdTable<UUID>("segment") {
    override val id = javaUUID("segment_id").entityId()
    val websiteId = javaUUID("website_id")
    val type = varchar("type", 50)
    val name = varchar("name", 200)
    val parameters = jsonb<String>("parameters", { it }, { it })
    val createdAt =
        pgTimestamptz("created_at", 6).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamptz("updated_at", 6).nullable().clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "segment_pkey")

    init {
        index("segment_website_id_idx", false, websiteId)
    }
}
