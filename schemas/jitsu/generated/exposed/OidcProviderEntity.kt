package jitsu.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class OidcProviderEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, OidcProviderEntity>(OidcProviderTable)

    var createdAt by OidcProviderTable.createdAt
    var updatedAt by OidcProviderTable.updatedAt
    var name by OidcProviderTable.name
    var issuer by OidcProviderTable.issuer
    var clientId by OidcProviderTable.clientId
    var clientSecret by OidcProviderTable.clientSecret
    var authorizationEndpoint by OidcProviderTable.authorizationEndpoint
    var tokenEndpoint by OidcProviderTable.tokenEndpoint
    var userinfoEndpoint by OidcProviderTable.userinfoEndpoint
    var jwksUri by OidcProviderTable.jwksUri
    var introspectionEndpoint by OidcProviderTable.introspectionEndpoint
    var scopes by OidcProviderTable.scopes
    var audience by OidcProviderTable.audience
    var prompt by OidcProviderTable.prompt
    var usernameClaim by OidcProviderTable.usernameClaim
    var emailClaim by OidcProviderTable.emailClaim
    var nameClaim by OidcProviderTable.nameClaim
    var groupClaim by OidcProviderTable.groupClaim
    var enabled by OidcProviderTable.enabled
    var autoDiscovery by OidcProviderTable.autoDiscovery
    val oidcLoginGroups by OidcLoginGroupEntity referrersOn OidcLoginGroupTable.oidcProviderId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(OidcProviderTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
