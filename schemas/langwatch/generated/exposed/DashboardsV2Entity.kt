package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class DashboardsV2Entity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, DashboardsV2Entity>(DashboardsV2Table)

    var projectId by DashboardsV2Table.projectId
    var name by DashboardsV2Table.name
    var description by DashboardsV2Table.description
    var createdById by DashboardsV2Table.createdById
    var scope by DashboardsV2Table.scope
    var organizationId by DashboardsV2Table.organizationId
    var excludedOrigins by DashboardsV2Table.excludedOrigins
    var createdAt by DashboardsV2Table.createdAt
    var updatedAt by DashboardsV2Table.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(DashboardsV2Table.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
