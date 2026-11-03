package documenso.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable

object OrganisationGroupTable : IdTable<String>("\"OrganisationGroup\"") {
    override val id = text("id").entityId()
    val name = text("name").nullable()
    val type = pgEnum("type", "\"OrganisationGroupType\"", OrganisationGroupType.entries, OrganisationGroupType::dbName)
    val organisationRole =
        pgEnum("organisationRole", "\"OrganisationMemberRole\"", OrganisationMemberRole.entries, OrganisationMemberRole::dbName)
    val organisationId =
        reference(
            "organisationId",
            OrganisationTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"OrganisationGroup_organisationId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"OrganisationGroup_pkey\"")

    init {
        index("\"OrganisationGroup_organisationId_idx\"", false, organisationId)
    }
}
