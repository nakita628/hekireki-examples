package hoppscotch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class TeamEnvironmentEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, TeamEnvironmentEntity>(TeamEnvironmentTable)

    var teamID by TeamEnvironmentTable.teamID
    var name by TeamEnvironmentTable.name
    var variables by TeamEnvironmentTable.variables
    var team by TeamEntity referencedOn TeamEnvironmentTable.teamID
}
