package papermark.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class InvitationEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, InvitationEntity>(InvitationTable)

    var email by InvitationTable.email
    var expires by InvitationTable.expires
    var teamId by InvitationTable.teamId
    var team by TeamEntity referencedOn InvitationTable.teamId
    var createdAt by InvitationTable.createdAt
    var role by InvitationTable.role
    var dataroomIds by InvitationTable.dataroomIds
}
