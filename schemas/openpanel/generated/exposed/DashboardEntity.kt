package openpanel.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class DashboardEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, DashboardEntity>(DashboardTable)

    var name by DashboardTable.name
    var organization by OrganizationEntity referencedOn DashboardTable.organizationId
    var organizationId by DashboardTable.organizationId
    var projectId by DashboardTable.projectId
    var project by ProjectEntity referencedOn DashboardTable.projectId
    val reports by ReportEntity referrersOn ReportTable.dashboardId
    val share by ShareDashboardEntity optionalBackReferencedOn ShareDashboardTable.dashboardId
    var createdAt by DashboardTable.createdAt
    var updatedAt by DashboardTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(DashboardTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
