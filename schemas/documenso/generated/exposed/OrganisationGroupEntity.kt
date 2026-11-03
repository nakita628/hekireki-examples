package documenso.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class OrganisationGroupEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, OrganisationGroupEntity>(OrganisationGroupTable)

    var name by OrganisationGroupTable.name
    var type by OrganisationGroupTable.type
    var organisationRole by OrganisationGroupTable.organisationRole
    var organisationId by OrganisationGroupTable.organisationId
    var organisation by OrganisationEntity referencedOn OrganisationGroupTable.organisationId
    val organisationGroupMembers by OrganisationGroupMemberEntity referrersOn OrganisationGroupMemberTable.groupId
    val teamGroups by TeamGroupEntity referrersOn TeamGroupTable.organisationGroupId
}
