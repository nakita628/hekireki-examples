package teable.models

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
    var baseId by DashboardTable.baseId
    var layout by DashboardTable.layout
    var createdBy by DashboardTable.createdBy
    var createdTime by DashboardTable.createdTime
    var lastModifiedTime by DashboardTable.lastModifiedTime
    var lastModifiedBy by DashboardTable.lastModifiedBy
    var deletedTime by DashboardTable.deletedTime

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(DashboardTable.lastModifiedTime)) {
                lastModifiedTime = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
