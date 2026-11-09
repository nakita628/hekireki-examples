package jitsu.models

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

    var createdAt by OAuthClientTable.createdAt
    var updatedAt by OAuthClientTable.updatedAt
    var clientSecretHash by OAuthClientTable.clientSecretHash
    var name by OAuthClientTable.name
    var redirectUris by OAuthClientTable.redirectUris
    val tokens by UserApiTokenEntity optionalReferrersOn UserApiTokenTable.oauthClientId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(OAuthClientTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
