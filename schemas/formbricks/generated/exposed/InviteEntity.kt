package formbricks.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class InviteEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, InviteEntity>(InviteTable)

    var email by InviteTable.email
    var name by InviteTable.name
    var organization by OrganizationEntity referencedOn InviteTable.organizationId
    var organizationId by InviteTable.organizationId
    var creator by UserEntity referencedOn InviteTable.creatorId
    var creatorId by InviteTable.creatorId
    var acceptor by UserEntity optionalReferencedOn InviteTable.acceptorId
    var acceptorId by InviteTable.acceptorId
    var createdAt by InviteTable.createdAt
    var expiresAt by InviteTable.expiresAt
    var role by InviteTable.role
    var teamIds by InviteTable.teamIds
}
