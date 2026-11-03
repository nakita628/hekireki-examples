package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object MastraFavoritesTable : CompositeIdTable("mastra_favorites") {
    val userId = text("userId").entityId()
    val entityType = text("entityType").entityId()
    val entityId = text("entityId").entityId()
    val createdAt = pgTimestamp("createdAt", 6)
    val createdAtZ =
        pgTimestamptz("createdAtZ", 6).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(userId, entityType, entityId, name = "mastra_favorites_pkey")

    init {
        index("idx_favorites_entity", false, entityType, entityId)
    }
}
