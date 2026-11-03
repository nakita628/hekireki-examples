package hoppscotch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class TeamMemberEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, TeamMemberEntity>(TeamMemberTable)

    var role by TeamMemberTable.role
    var userUid by TeamMemberTable.userUid
    var teamID by TeamMemberTable.teamID
    var team by TeamEntity referencedOn TeamMemberTable.teamID
}
