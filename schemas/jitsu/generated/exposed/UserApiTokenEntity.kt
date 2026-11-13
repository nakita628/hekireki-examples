package jitsu.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class UserApiTokenEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, UserApiTokenEntity>(UserApiTokenTable)

    var createdAt by UserApiTokenTable.createdAt
    var updatedAt by UserApiTokenTable.updatedAt
    var lastUsed by UserApiTokenTable.lastUsed
    var hint by UserApiTokenTable.hint
    var hash by UserApiTokenTable.hash
    var userId by UserApiTokenTable.userId
    var user by UserProfileEntity referencedOn UserApiTokenTable.userId
    var type by UserApiTokenTable.type
    var name by UserApiTokenTable.name
    var expiresAt by UserApiTokenTable.expiresAt
    var workspaceId by UserApiTokenTable.workspaceId
    var role by UserApiTokenTable.role
    var oauthClientId by UserApiTokenTable.oauthClientId
    var oauthClient by OAuthClientEntity optionalReferencedOn UserApiTokenTable.oauthClientId
    val accessTokens by OAuthAccessTokenEntity referrersOn OAuthAccessTokenTable.refreshTokenId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(UserApiTokenTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
