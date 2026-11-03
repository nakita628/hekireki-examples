package triggerdev.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class MetricsDashboardEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, MetricsDashboardEntity>(MetricsDashboardTable)

    var friendlyId by MetricsDashboardTable.friendlyId
    var title by MetricsDashboardTable.title
    var description by MetricsDashboardTable.description
    var organization by OrganizationEntity referencedOn MetricsDashboardTable.organizationId
    var organizationId by MetricsDashboardTable.organizationId
    var project by ProjectEntity optionalReferencedOn MetricsDashboardTable.projectId
    var projectId by MetricsDashboardTable.projectId
    var owner by UserEntity optionalReferencedOn MetricsDashboardTable.ownerId
    var ownerId by MetricsDashboardTable.ownerId
    var createdAt by MetricsDashboardTable.createdAt
    var updatedAt by MetricsDashboardTable.updatedAt
    var layout by MetricsDashboardTable.layout

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(MetricsDashboardTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
