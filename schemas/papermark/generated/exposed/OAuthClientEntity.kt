package papermark.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class OAuthClientEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, OAuthClientEntity>(OAuthClientTable)

    var clientId by OAuthClientTable.clientId
    var clientName by OAuthClientTable.clientName
    var clientUri by OAuthClientTable.clientUri
    var logoUri by OAuthClientTable.logoUri
    var redirectUris by OAuthClientTable.redirectUris
    var grantTypes by OAuthClientTable.grantTypes
    var scopes by OAuthClientTable.scopes
    var tokenAuthMethod by OAuthClientTable.tokenAuthMethod
    var hashedSecret by OAuthClientTable.hashedSecret
    var registrationType by OAuthClientTable.registrationType
    var metadataUrl by OAuthClientTable.metadataUrl
    var createdAt by OAuthClientTable.createdAt
    var updatedAt by OAuthClientTable.updatedAt
    val tokens by RestrictedTokenEntity optionalReferrersOn RestrictedTokenTable.clientId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(OAuthClientTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
