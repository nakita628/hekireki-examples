package formbricks.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass

class MembershipEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<MembershipEntity>(MembershipTable)

    val organization by OrganizationEntity referencedOn MembershipTable.organizationId
    val organizationId by MembershipTable.organizationId
    val user by UserEntity referencedOn MembershipTable.userId
    val userId by MembershipTable.userId
    var accepted by MembershipTable.accepted
    var role by MembershipTable.role
}
