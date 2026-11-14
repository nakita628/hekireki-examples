package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object DashboardFavouriteTable : IdTable<String>("\"DashboardFavourite\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val userId = text("userId")
    val dashboardId = text("dashboardId").nullable()
    val templateId = text("templateId").nullable()
    val projectId = text("projectId")
    val position = integer("position")
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"DashboardFavourite_pkey\"")

    init {
        index("\"DashboardFavourite_userId_projectId_position_idx\"", false, userId, projectId, position)
        index("\"DashboardFavourite_dashboardId_idx\"", false, dashboardId)
        uniqueIndex("\"DashboardFavourite_userId_dashboardId_key\"", userId, dashboardId)
        uniqueIndex("\"DashboardFavourite_userId_projectId_templateId_key\"", userId, projectId, templateId)
    }
}
