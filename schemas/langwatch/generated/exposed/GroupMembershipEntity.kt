package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass

class GroupMembershipEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<GroupMembershipEntity>(GroupMembershipTable)

    val userId by GroupMembershipTable.userId
    val groupId by GroupMembershipTable.groupId
    var createdAt by GroupMembershipTable.createdAt
}
