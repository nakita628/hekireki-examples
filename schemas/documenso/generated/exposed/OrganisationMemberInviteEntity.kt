package documenso.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class OrganisationMemberInviteEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, OrganisationMemberInviteEntity>(OrganisationMemberInviteTable)

    var createdAt by OrganisationMemberInviteTable.createdAt
    var email by OrganisationMemberInviteTable.email
    var token by OrganisationMemberInviteTable.token
    var status by OrganisationMemberInviteTable.status
    var organisationId by OrganisationMemberInviteTable.organisationId
    var organisation by OrganisationEntity referencedOn OrganisationMemberInviteTable.organisationId
    var organisationRole by OrganisationMemberInviteTable.organisationRole
}
