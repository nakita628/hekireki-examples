package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class InvitationEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, InvitationEntity>(InvitationTable)

    var organizationId by InvitationTable.organizationId
    var organization by OrganizationEntity referencedOn InvitationTable.organizationId
    var email by InvitationTable.email
    var role by InvitationTable.role
    var status by InvitationTable.status
    var expiresAt by InvitationTable.expiresAt
    var inviterId by InvitationTable.inviterId
    var inviter by EmailAccountEntity referencedOn InvitationTable.inviterId
}
