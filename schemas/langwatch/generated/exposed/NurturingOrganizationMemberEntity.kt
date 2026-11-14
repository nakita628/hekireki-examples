package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass

class NurturingOrganizationMemberEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<NurturingOrganizationMemberEntity>(NurturingOrganizationMemberTable)

    val organizationId by NurturingOrganizationMemberTable.organizationId
    val userId by NurturingOrganizationMemberTable.userId
    var joinedAt by NurturingOrganizationMemberTable.joinedAt
    var leftAt by NurturingOrganizationMemberTable.leftAt
    var returnedAt by NurturingOrganizationMemberTable.returnedAt
}
