package documenso.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class OrganisationGroupMemberEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, OrganisationGroupMemberEntity>(OrganisationGroupMemberTable)

    var groupId by OrganisationGroupMemberTable.groupId
    var group by OrganisationGroupEntity referencedOn OrganisationGroupMemberTable.groupId
    var organisationMember by OrganisationMemberEntity referencedOn OrganisationGroupMemberTable.organisationMemberId
    var organisationMemberId by OrganisationGroupMemberTable.organisationMemberId
}
