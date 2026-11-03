package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object MastraChannelConfigTable : IdTable<String>("mastra_channel_config") {
    override val id = text("platform").entityId()
    val data = jsonb<String>("data", { it }, { it })
    val updatedAt = pgTimestamp("updatedAt", 6)
    val updatedAtZ =
        pgTimestamptz("updatedAtZ", 6).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "mastra_channel_config_pkey")
}
