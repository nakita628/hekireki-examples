package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class DSyncDataEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, DSyncDataEntity>(DSyncDataTable)

    var directoryId by DSyncDataTable.directoryId
    var tenant by DSyncDataTable.tenant
    var organizationId by DSyncDataTable.organizationId
    val teamGroupMapping by DSyncTeamGroupMappingEntity referrersOn DSyncTeamGroupMappingTable.directoryId
    val createdAttributeToUsers by AttributeToUserEntity optionalReferrersOn AttributeToUserTable.createdByDSyncId
    val updatedAttributeToUsers by AttributeToUserEntity optionalReferrersOn AttributeToUserTable.updatedByDSyncId
}
