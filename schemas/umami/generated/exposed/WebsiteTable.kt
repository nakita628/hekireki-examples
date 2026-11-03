package umami.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.core.java.javaUUID
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object WebsiteTable : IdTable<UUID>("website") {
    override val id = javaUUID("website_id").entityId()
    val name = varchar("name", 100)
    val domain = varchar("domain", 500).nullable()
    val resetAt = pgTimestamptz("reset_at", 6).nullable()
    val userId = javaUUID("user_id").nullable()
    val teamId = javaUUID("team_id").nullable()
    val createdBy = javaUUID("created_by").nullable()
    val createdAt =
        pgTimestamptz("created_at", 6).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamptz("updated_at", 6).nullable().clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val deletedAt = pgTimestamptz("deleted_at", 6).nullable()
    val recorderEnabled = bool("recorder_enabled").default(false)
    val replayConfig = jsonb<String>("replay_config", { it }, { it }).nullable()
    override val primaryKey = PrimaryKey(id, name = "website_pkey")

    init {
        index("website_user_id_idx", false, userId)
        index("website_team_id_idx", false, teamId)
        index("website_created_at_idx", false, createdAt)
        index("website_created_by_idx", false, createdBy)
    }
}
