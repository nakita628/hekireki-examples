package documenso.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable

object OrganisationGroupMemberTable : IdTable<String>("\"OrganisationGroupMember\"") {
    override val id = text("id").entityId()
    val groupId =
        reference(
            "groupId",
            OrganisationGroupTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"OrganisationGroupMember_groupId_fkey\"",
        )
    val organisationMemberId =
        reference(
            "organisationMemberId",
            OrganisationMemberTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"OrganisationGroupMember_organisationMemberId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"OrganisationGroupMember_pkey\"")

    init {
        index("\"OrganisationGroupMember_groupId_idx\"", false, groupId)
        index("\"OrganisationGroupMember_organisationMemberId_idx\"", false, organisationMemberId)
        uniqueIndex("\"OrganisationGroupMember_organisationMemberId_groupId_key\"", organisationMemberId, groupId)
    }
}
