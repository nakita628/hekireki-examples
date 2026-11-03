package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class OAuthAppEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, OAuthAppEntity>(OAuthAppTable)

    var organizationId by OAuthAppTable.organizationId
    var name by OAuthAppTable.name
    var description by OAuthAppTable.description
    var pictureId by OAuthAppTable.pictureId
    var redirectUrl by OAuthAppTable.redirectUrl
    var redirectUris by OAuthAppTable.redirectUris
    var clientId by OAuthAppTable.clientId
    var clientSecret by OAuthAppTable.clientSecret
    var dynamic by OAuthAppTable.dynamic
    var tokenEndpointAuthMethod by OAuthAppTable.tokenEndpointAuthMethod
    var createdAt by OAuthAppTable.createdAt
    var updatedAt by OAuthAppTable.updatedAt
    var deletedAt by OAuthAppTable.deletedAt
    var organization by OrganizationEntity optionalReferencedOn OAuthAppTable.organizationId
    var picture by MediaEntity optionalReferencedOn OAuthAppTable.pictureId
    val authorizations by OAuthAuthorizationEntity referrersOn OAuthAuthorizationTable.oauthAppId
    val selfHostedAuthorizations by OAuthSelfHostedAuthorizationEntity referrersOn OAuthSelfHostedAuthorizationTable.oauthAppId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(OAuthAppTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
