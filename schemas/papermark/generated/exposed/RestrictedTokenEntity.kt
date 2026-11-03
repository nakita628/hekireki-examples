package papermark.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class RestrictedTokenEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, RestrictedTokenEntity>(RestrictedTokenTable)

    var name by RestrictedTokenTable.name
    var hashedKey by RestrictedTokenTable.hashedKey
    var partialKey by RestrictedTokenTable.partialKey
    var scopes by RestrictedTokenTable.scopes
    var mode by RestrictedTokenTable.mode
    var source by RestrictedTokenTable.source1
    var subjectType by RestrictedTokenTable.subjectType
    var clientId by RestrictedTokenTable.clientId
    var expires by RestrictedTokenTable.expires
    var lastUsed by RestrictedTokenTable.lastUsed
    var rateLimit by RestrictedTokenTable.rateLimit
    var userId by RestrictedTokenTable.userId
    var teamId by RestrictedTokenTable.teamId
    var user by UserEntity referencedOn RestrictedTokenTable.userId
    var team by TeamEntity referencedOn RestrictedTokenTable.teamId
    var oauthClient by OAuthClientEntity optionalReferencedOn RestrictedTokenTable.clientId
    var createdAt by RestrictedTokenTable.createdAt
    var updatedAt by RestrictedTokenTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(RestrictedTokenTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
