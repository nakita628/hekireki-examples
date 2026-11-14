package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class SsoProviderEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, SsoProviderEntity>(SsoProviderTable)

    var issuer by SsoProviderTable.issuer
    var oidcConfig by SsoProviderTable.oidcConfig
    var samlConfig by SsoProviderTable.samlConfig
    var userId by SsoProviderTable.userId
    var providerId by SsoProviderTable.providerId
    var organizationId by SsoProviderTable.organizationId
    var domain by SsoProviderTable.domain
}
