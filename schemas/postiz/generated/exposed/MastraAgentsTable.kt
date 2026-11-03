package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object MastraAgentsTable : IdTable<String>("mastra_agents") {
    override val id = text("id").entityId()
    val status = text("status")
    val activeVersionId = text("activeVersionId").nullable()
    val authorId = text("authorId").nullable()
    val metadata = jsonb<String>("metadata", { it }, { it }).nullable()
    val createdAt = pgTimestamp("createdAt", 6)
    val updatedAt = pgTimestamp("updatedAt", 6)
    val createdAtZ =
        pgTimestamptz("createdAtZ", 6).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAtZ =
        pgTimestamptz("updatedAtZ", 6).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val visibility = text("visibility").nullable()
    val favoriteCount = integer("favoriteCount").nullable()
    override val primaryKey = PrimaryKey(id, name = "mastra_agents_pkey")
}
