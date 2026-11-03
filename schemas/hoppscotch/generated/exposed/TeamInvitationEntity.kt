package hoppscotch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class TeamInvitationEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, TeamInvitationEntity>(TeamInvitationTable)

    var teamID by TeamInvitationTable.teamID
    var creatorUid by TeamInvitationTable.creatorUid
    var inviteeEmail by TeamInvitationTable.inviteeEmail
    var inviteeRole by TeamInvitationTable.inviteeRole
    var team by TeamEntity referencedOn TeamInvitationTable.teamID
}
