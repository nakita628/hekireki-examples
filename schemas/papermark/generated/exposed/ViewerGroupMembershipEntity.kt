package papermark.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class ViewerGroupMembershipEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ViewerGroupMembershipEntity>(ViewerGroupMembershipTable)

    var viewerId by ViewerGroupMembershipTable.viewerId
    var viewer by ViewerEntity referencedOn ViewerGroupMembershipTable.viewerId
    var groupId by ViewerGroupMembershipTable.groupId
    var group by ViewerGroupEntity referencedOn ViewerGroupMembershipTable.groupId
    var createdAt by ViewerGroupMembershipTable.createdAt
    var updatedAt by ViewerGroupMembershipTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ViewerGroupMembershipTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
