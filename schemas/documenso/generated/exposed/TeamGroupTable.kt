package documenso.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable

object TeamGroupTable : IdTable<String>("\"TeamGroup\"") {
    override val id = text("id").entityId()
    val organisationGroupId =
        reference(
            "organisationGroupId",
            OrganisationGroupTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"TeamGroup_organisationGroupId_fkey\"",
        )
    val teamRole = pgEnum("teamRole", "\"TeamMemberRole\"", TeamMemberRole.entries, TeamMemberRole::dbName)
    val teamId =
        reference(
            "teamId",
            TeamTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"TeamGroup_teamId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"TeamGroup_pkey\"")

    init {
        index("\"TeamGroup_teamId_idx\"", false, teamId)
        index("\"TeamGroup_organisationGroupId_idx\"", false, organisationGroupId)
        uniqueIndex("\"TeamGroup_teamId_organisationGroupId_key\"", teamId, organisationGroupId)
    }
}
