package documenso.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class OrganisationAuthenticationPortalEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, OrganisationAuthenticationPortalEntity>(OrganisationAuthenticationPortalTable)

    val organisation by OrganisationEntity optionalBackReferencedOn OrganisationTable.organisationAuthenticationPortalId
    var enabled by OrganisationAuthenticationPortalTable.enabled
    var clientId by OrganisationAuthenticationPortalTable.clientId
    var clientSecret by OrganisationAuthenticationPortalTable.clientSecret
    var wellKnownUrl by OrganisationAuthenticationPortalTable.wellKnownUrl
    var defaultOrganisationRole by OrganisationAuthenticationPortalTable.defaultOrganisationRole
    var autoProvisionUsers by OrganisationAuthenticationPortalTable.autoProvisionUsers
    var allowedDomains by OrganisationAuthenticationPortalTable.allowedDomains
    var allowPersonalOrganisations by OrganisationAuthenticationPortalTable.allowPersonalOrganisations
}
