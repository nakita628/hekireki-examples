package langfuse.models

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
    var createdBy by DashboardTable.createdBy
    var createdByUser by UserEntity optionalReferencedOn DashboardTable.createdBy
    var updatedBy by DashboardTable.updatedBy
    var updatedByUser by UserEntity optionalReferencedOn DashboardTable.updatedBy
    var projectId by DashboardTable.projectId
    var project by ProjectEntity optionalReferencedOn DashboardTable.projectId
    var name by DashboardTable.name
    var description by DashboardTable.description
    var definition by DashboardTable.definition
    var filters by DashboardTable.filters
    val homeDashboardProjects by ProjectEntity optionalReferrersOn ProjectTable.homeDashboardId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(DashboardTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
