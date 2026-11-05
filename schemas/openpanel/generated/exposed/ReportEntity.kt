package openpanel.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

class ReportEntity(
    id: EntityID<UUID>,
) : Entity<UUID>(id) {
    companion object : EntityClass<UUID, ReportEntity>(ReportTable)

    var name by ReportTable.name
    var interval by ReportTable.interval
    var range by ReportTable.range
    var chartType by ReportTable.chartType
    var lineType by ReportTable.lineType
    var breakdowns by ReportTable.breakdowns
    var events by ReportTable.events
    var globalFilters by ReportTable.globalFilters
    var formula by ReportTable.formula
    var unit by ReportTable.unit
    var metric by ReportTable.metric
    var projectId by ReportTable.projectId
    var project by ProjectEntity referencedOn ReportTable.projectId
    var previous by ReportTable.previous
    var criteria by ReportTable.criteria
    var funnelGroup by ReportTable.funnelGroup
    var funnelWindow by ReportTable.funnelWindow
    var options by ReportTable.options1
    var display by ReportTable.display
    var visibleSeries by ReportTable.visibleSeries
    var startDate by ReportTable.startDate
    var endDate by ReportTable.endDate
    var dashboardId by ReportTable.dashboardId
    var dashboard by DashboardEntity referencedOn ReportTable.dashboardId
    val layout by ReportLayoutEntity optionalBackReferencedOn ReportLayoutTable.reportId
    val share by ShareReportEntity optionalBackReferencedOn ShareReportTable.reportId
    var createdAt by ReportTable.createdAt
    var updatedAt by ReportTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ReportTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
