package umami.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.core.java.javaUUID
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object TeamUserTable : IdTable<UUID>("team_user") {
    override val id = javaUUID("team_user_id").entityId()
    val teamId = javaUUID("team_id")
    val userId = javaUUID("user_id")
    val role = varchar("role", 50)
    val createdAt =
        pgTimestamptz("created_at", 6).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamptz("updated_at", 6).nullable().clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "team_user_pkey")

    init {
        index("team_user_team_id_idx", false, teamId)
        index("team_user_user_id_idx", false, userId)
    }
}
