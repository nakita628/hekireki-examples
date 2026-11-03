package calcom.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable

object DSyncTeamGroupMappingTable : IdTable<Int>("\"DSyncTeamGroupMapping\"") {
    override val id = integer("id").autoIncrement().entityId()
    val organizationId = integer("organizationId")
    val teamId =
        reference(
            "teamId",
            TeamTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"DSyncTeamGroupMapping_teamId_fkey\"",
        )
    val directoryId =
        text("directoryId").references(
            DSyncDataTable.directoryId,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"DSyncTeamGroupMapping_directoryId_fkey\"",
        )
    val groupName = text("groupName")
    override val primaryKey = PrimaryKey(id, name = "\"DSyncTeamGroupMapping_pkey\"")

    init {
        uniqueIndex("\"DSyncTeamGroupMapping_teamId_groupName_key\"", teamId, groupName)
    }
}
