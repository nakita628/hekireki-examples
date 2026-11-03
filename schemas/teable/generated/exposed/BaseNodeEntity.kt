package teable.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class BaseNodeEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, BaseNodeEntity>(BaseNodeTable)

    var parentId by BaseNodeTable.parentId
    var baseId by BaseNodeTable.baseId
    var resourceType by BaseNodeTable.resourceType
    var resourceId by BaseNodeTable.resourceId
    var order by BaseNodeTable.order
    var createdTime by BaseNodeTable.createdTime
    var createdBy by BaseNodeTable.createdBy
    var lastModifiedTime by BaseNodeTable.lastModifiedTime
    var lastModifiedBy by BaseNodeTable.lastModifiedBy
    var parent by BaseNodeEntity optionalReferencedOn BaseNodeTable.parentId
    val children by BaseNodeEntity optionalReferrersOn BaseNodeTable.parentId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(BaseNodeTable.lastModifiedTime)) {
                lastModifiedTime = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
