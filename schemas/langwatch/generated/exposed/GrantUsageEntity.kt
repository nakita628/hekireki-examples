package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class GrantUsageEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, GrantUsageEntity>(GrantUsageTable)

    var organizationId by GrantUsageTable.organizationId
    var projectId by GrantUsageTable.projectId
    var viewCount by GrantUsageTable.viewCount
    var lastViewedAt by GrantUsageTable.lastViewedAt
    var createdAt by GrantUsageTable.createdAt
    var updatedAt by GrantUsageTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(GrantUsageTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
