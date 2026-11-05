package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class SsoProviderEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, SsoProviderEntity>(SsoProviderTable)

    var userId by SsoProviderTable.userId
    var user by UserEntity optionalReferencedOn SsoProviderTable.userId
    var issuer by SsoProviderTable.issuer
    var oidcConfig by SsoProviderTable.oidcConfig
    var samlConfig by SsoProviderTable.samlConfig
    var emailAccountId by SsoProviderTable.emailAccountId
    var emailAccount by EmailAccountEntity optionalReferencedOn SsoProviderTable.emailAccountId
    var providerId by SsoProviderTable.providerId
    var organizationId by SsoProviderTable.organizationId
    var organization by OrganizationEntity optionalReferencedOn SsoProviderTable.organizationId
    var domain by SsoProviderTable.domain
}
