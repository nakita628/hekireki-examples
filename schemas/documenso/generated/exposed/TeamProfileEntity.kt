package documenso.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class TeamProfileEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, TeamProfileEntity>(TeamProfileTable)

    var enabled by TeamProfileTable.enabled
    var teamId by TeamProfileTable.teamId
    var bio by TeamProfileTable.bio
    var team by TeamEntity referencedOn TeamProfileTable.teamId
}
