package hoppscotch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class TeamEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, TeamEntity>(TeamTable)

    var name by TeamTable.name
    val collections by TeamCollectionEntity referrersOn TeamCollectionTable.teamID
    val environments by TeamEnvironmentEntity referrersOn TeamEnvironmentTable.teamID
    val invitations by TeamInvitationEntity referrersOn TeamInvitationTable.teamID
    val members by TeamMemberEntity referrersOn TeamMemberTable.teamID
    val requests by TeamRequestEntity referrersOn TeamRequestTable.teamID
}
