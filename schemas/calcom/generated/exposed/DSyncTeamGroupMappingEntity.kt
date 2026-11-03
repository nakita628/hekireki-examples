package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class DSyncTeamGroupMappingEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, DSyncTeamGroupMappingEntity>(DSyncTeamGroupMappingTable)

    var organizationId by DSyncTeamGroupMappingTable.organizationId
    var teamId by DSyncTeamGroupMappingTable.teamId
    var team by TeamEntity referencedOn DSyncTeamGroupMappingTable.teamId
    var directoryId by DSyncTeamGroupMappingTable.directoryId
    var directory by DSyncDataEntity referencedOn DSyncTeamGroupMappingTable.directoryId
    var groupName by DSyncTeamGroupMappingTable.groupName
}
