package papermark.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass

class UserTeamEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<UserTeamEntity>(UserTeamTable)

    var role by UserTeamTable.role
    var status by UserTeamTable.status
    val userId by UserTeamTable.userId
    val teamId by UserTeamTable.teamId
    val user by UserEntity referencedOn UserTeamTable.userId
    val team by TeamEntity referencedOn UserTeamTable.teamId
    var blockedAt by UserTeamTable.blockedAt
    var notificationPreferences by UserTeamTable.notificationPreferences
}
