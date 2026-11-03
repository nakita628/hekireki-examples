package formbricks.models

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

    var createdAt by DashboardTable.createdAt
    var updatedAt by DashboardTable.updatedAt
    var name by DashboardTable.name
    var workspace by WorkspaceEntity referencedOn DashboardTable.workspaceId
    var workspaceId by DashboardTable.workspaceId
    var creator by UserEntity optionalReferencedOn DashboardTable.createdBy
    var createdBy by DashboardTable.createdBy
    val widgets by DashboardWidgetEntity referrersOn DashboardWidgetTable.dashboardId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(DashboardTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
