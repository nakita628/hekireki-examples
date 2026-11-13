package rallly.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import java.time.Instant
import java.time.temporal.ChronoUnit

class ApiUsageEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<ApiUsageEntity>(ApiUsageTable)

    val spaceId by ApiUsageTable.spaceId
    val resource by ApiUsageTable.resource
    val period by ApiUsageTable.period
    var count by ApiUsageTable.count
    var nearLimitWarnedAt by ApiUsageTable.nearLimitWarnedAt
    var atLimitWarnedAt by ApiUsageTable.atLimitWarnedAt
    var updatedAt by ApiUsageTable.updatedAt
    val space by SpaceEntity referencedOn ApiUsageTable.spaceId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ApiUsageTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
