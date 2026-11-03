package teable.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class SpaceDataDbBindingEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, SpaceDataDbBindingEntity>(SpaceDataDbBindingTable)

    var spaceId by SpaceDataDbBindingTable.spaceId
    var dataDbConnectionId by SpaceDataDbBindingTable.dataDbConnectionId
    var mode by SpaceDataDbBindingTable.mode
    var state by SpaceDataDbBindingTable.state
    var createdBy by SpaceDataDbBindingTable.createdBy
    var createdTime by SpaceDataDbBindingTable.createdTime
    var lastModifiedTime by SpaceDataDbBindingTable.lastModifiedTime
    var lastHistoryFlushedAt by SpaceDataDbBindingTable.lastHistoryFlushedAt
    var lastRemovalFlushedAt by SpaceDataDbBindingTable.lastRemovalFlushedAt
    var space by SpaceEntity referencedOn SpaceDataDbBindingTable.spaceId
    var dataDbConnection by DataDbConnectionEntity optionalReferencedOn SpaceDataDbBindingTable.dataDbConnectionId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(SpaceDataDbBindingTable.lastModifiedTime)) {
                lastModifiedTime = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
