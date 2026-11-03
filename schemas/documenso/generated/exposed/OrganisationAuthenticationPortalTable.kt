package documenso.models

import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable

object OrganisationAuthenticationPortalTable : IdTable<String>("\"OrganisationAuthenticationPortal\"") {
    override val id = text("id").entityId()
    val enabled = bool("enabled").default(false)
    val clientId = text("clientId").default("")
    val clientSecret = text("clientSecret").default("")
    val wellKnownUrl = text("wellKnownUrl").default("")
    val defaultOrganisationRole =
        pgEnum("defaultOrganisationRole", "\"OrganisationMemberRole\"", OrganisationMemberRole.entries, OrganisationMemberRole::dbName)
            .default(OrganisationMemberRole.MEMBER)
    val autoProvisionUsers = bool("autoProvisionUsers").default(true)
    val allowedDomains = array("allowedDomains", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val allowPersonalOrganisations = bool("allowPersonalOrganisations").default(false)
    override val primaryKey = PrimaryKey(id, name = "\"OrganisationAuthenticationPortal_pkey\"")
}
