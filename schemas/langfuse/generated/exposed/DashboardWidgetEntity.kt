package langfuse.models

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

    var createdAt by DashboardWidgetTable.createdAt
    var updatedAt by DashboardWidgetTable.updatedAt
    var createdBy by DashboardWidgetTable.createdBy
    var createdByUser by UserEntity optionalReferencedOn DashboardWidgetTable.createdBy
    var updatedBy by DashboardWidgetTable.updatedBy
    var updatedByUser by UserEntity optionalReferencedOn DashboardWidgetTable.updatedBy
    var projectId by DashboardWidgetTable.projectId
    var project by ProjectEntity optionalReferencedOn DashboardWidgetTable.projectId
    var name by DashboardWidgetTable.name
    var description by DashboardWidgetTable.description
    var view by DashboardWidgetTable.view
    var dimensions by DashboardWidgetTable.dimensions
    var metrics by DashboardWidgetTable.metrics
    var filters by DashboardWidgetTable.filters
    var chartType by DashboardWidgetTable.chartType
    var chartConfig by DashboardWidgetTable.chartConfig
    var minVersion by DashboardWidgetTable.minVersion

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(DashboardWidgetTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
