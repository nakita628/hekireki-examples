package teable.models

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

    var name by OAuthAppTable.name
    var logo by OAuthAppTable.logo
    var homepage by OAuthAppTable.homepage
    var description by OAuthAppTable.description
    var clientId by OAuthAppTable.clientId
    var redirectUris by OAuthAppTable.redirectUris
    var scopes by OAuthAppTable.scopes
    var allowDeviceFlow by OAuthAppTable.allowDeviceFlow
    var dynamicRegistration by OAuthAppTable.dynamicRegistration
    var createdTime by OAuthAppTable.createdTime
    var lastModifiedTime by OAuthAppTable.lastModifiedTime
    var createdBy by OAuthAppTable.createdBy
    val secrets by OAuthAppSecretEntity referrersOn OAuthAppSecretTable.clientId
    val authorized by OAuthAppAuthorizedEntity referrersOn OAuthAppAuthorizedTable.clientId
    val tokens by OAuthAppTokenEntity referrersOn OAuthAppTokenTable.clientId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(OAuthAppTable.lastModifiedTime)) {
                lastModifiedTime = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
