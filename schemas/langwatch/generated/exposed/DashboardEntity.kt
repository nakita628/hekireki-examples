package langwatch.models

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

    var projectId by DashboardTable.projectId
    var name by DashboardTable.name
    var order by DashboardTable.order
    var description by DashboardTable.description
    var createdById by DashboardTable.createdById
    var scope by DashboardTable.scope
    var organizationId by DashboardTable.organizationId
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
