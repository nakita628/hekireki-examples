package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class DashboardWidgetEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, DashboardWidgetEntity>(DashboardWidgetTable)

    var projectId by DashboardWidgetTable.projectId
    var dashboardId by DashboardWidgetTable.dashboardId
    var name by DashboardWidgetTable.name
    var definition by DashboardWidgetTable.definition
    var gridColumn by DashboardWidgetTable.gridColumn
    var gridRow by DashboardWidgetTable.gridRow
    var colSpan by DashboardWidgetTable.colSpan
    var rowSpan by DashboardWidgetTable.rowSpan
    var createdAt by DashboardWidgetTable.createdAt
    var updatedAt by DashboardWidgetTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(DashboardWidgetTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
