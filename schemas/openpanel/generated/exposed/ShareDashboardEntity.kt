package openpanel.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class ShareDashboardEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ShareDashboardEntity>(ShareDashboardTable)

    var dashboardId by ShareDashboardTable.dashboardId
    var dashboard by DashboardEntity referencedOn ShareDashboardTable.dashboardId
    var organization by OrganizationEntity referencedOn ShareDashboardTable.organizationId
    var organizationId by ShareDashboardTable.organizationId
    var projectId by ShareDashboardTable.projectId
    var project by ProjectEntity referencedOn ShareDashboardTable.projectId
    var public by ShareDashboardTable.public
    var password by ShareDashboardTable.password
    var createdAt by ShareDashboardTable.createdAt
    var updatedAt by ShareDashboardTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ShareDashboardTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
