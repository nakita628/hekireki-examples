package teable.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class BaseDataDbMoveJobEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, BaseDataDbMoveJobEntity>(BaseDataDbMoveJobTable)

    var baseId by BaseDataDbMoveJobTable.baseId
    var sourceSpaceId by BaseDataDbMoveJobTable.sourceSpaceId
    var targetSpaceId by BaseDataDbMoveJobTable.targetSpaceId
    var sourceConnectionId by BaseDataDbMoveJobTable.sourceConnectionId
    var targetConnectionId by BaseDataDbMoveJobTable.targetConnectionId
    var state by BaseDataDbMoveJobTable.state
    var inventory by BaseDataDbMoveJobTable.inventory
    var copyStats by BaseDataDbMoveJobTable.copyStats
    var validationStats by BaseDataDbMoveJobTable.validationStats
    var lastError by BaseDataDbMoveJobTable.lastError
    var startedAt by BaseDataDbMoveJobTable.startedAt
    var completedAt by BaseDataDbMoveJobTable.completedAt
    var createdBy by BaseDataDbMoveJobTable.createdBy
    var createdTime by BaseDataDbMoveJobTable.createdTime
    var lastModifiedTime by BaseDataDbMoveJobTable.lastModifiedTime
    var base by BaseEntity referencedOn BaseDataDbMoveJobTable.baseId
    var sourceSpace by SpaceEntity referencedOn BaseDataDbMoveJobTable.sourceSpaceId
    var targetSpace by SpaceEntity referencedOn BaseDataDbMoveJobTable.targetSpaceId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(BaseDataDbMoveJobTable.lastModifiedTime)) {
                lastModifiedTime = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
