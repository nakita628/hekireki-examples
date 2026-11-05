package openpanel.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass

class MemberProjectEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<MemberProjectEntity>(MemberProjectTable)

    val memberId by MemberProjectTable.memberId
    val member by MemberEntity referencedOn MemberProjectTable.memberId
    val projectId by MemberProjectTable.projectId
    val project by ProjectEntity referencedOn MemberProjectTable.projectId
    var level by MemberProjectTable.level
    var createdAt by MemberProjectTable.createdAt
}
