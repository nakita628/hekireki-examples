package documenso.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class TeamGroupEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, TeamGroupEntity>(TeamGroupTable)

    var organisationGroupId by TeamGroupTable.organisationGroupId
    var organisationGroup by OrganisationGroupEntity referencedOn TeamGroupTable.organisationGroupId
    var teamRole by TeamGroupTable.teamRole
    var teamId by TeamGroupTable.teamId
    var team by TeamEntity referencedOn TeamGroupTable.teamId
}
