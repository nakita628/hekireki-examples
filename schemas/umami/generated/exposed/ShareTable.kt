package umami.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.core.java.javaUUID
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object ShareTable : IdTable<UUID>("share") {
    override val id = javaUUID("share_id").entityId()
    val entityId = javaUUID("entity_id")
    val name = varchar("name", 200)
    val shareType = integer("share_type")
    val slug = varchar("slug", 100).uniqueIndex("share_slug_key")
    val parameters = jsonb<String>("parameters", { it }, { it })
    val createdAt =
        pgTimestamptz("created_at", 6).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamptz("updated_at", 6).nullable().clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "share_pkey")

    init {
        index("share_entity_id_idx", false, entityId)
    }
}
