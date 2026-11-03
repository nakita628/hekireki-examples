package umami.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.core.java.javaUUID
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object BoardTable : IdTable<UUID>("board") {
    override val id = javaUUID("board_id").entityId()
    val type = varchar("type", 50)
    val name = varchar("name", 200)
    val description = varchar("description", 500)
    val parameters = jsonb<String>("parameters", { it }, { it })
    val userId = javaUUID("user_id").nullable()
    val teamId = javaUUID("team_id").nullable()
    val createdAt =
        pgTimestamptz("created_at", 6).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamptz("updated_at", 6).nullable().clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "board_pkey")

    init {
        index("board_user_id_idx", false, userId)
        index("board_team_id_idx", false, teamId)
        index("board_created_at_idx", false, createdAt)
    }
}
