package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class DashboardFavouriteEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, DashboardFavouriteEntity>(DashboardFavouriteTable)

    var userId by DashboardFavouriteTable.userId
    var dashboardId by DashboardFavouriteTable.dashboardId
    var templateId by DashboardFavouriteTable.templateId
    var projectId by DashboardFavouriteTable.projectId
    var position by DashboardFavouriteTable.position
    var createdAt by DashboardFavouriteTable.createdAt
}
