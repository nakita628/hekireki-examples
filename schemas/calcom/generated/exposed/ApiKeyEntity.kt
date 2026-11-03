package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class ApiKeyEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ApiKeyEntity>(ApiKeyTable)

    var userId by ApiKeyTable.userId
    var teamId by ApiKeyTable.teamId
    var note by ApiKeyTable.note
    var createdAt by ApiKeyTable.createdAt
    var expiresAt by ApiKeyTable.expiresAt
    var lastUsedAt by ApiKeyTable.lastUsedAt
    var hashedKey by ApiKeyTable.hashedKey
    var user by UserEntity referencedOn ApiKeyTable.userId
    var team by TeamEntity optionalReferencedOn ApiKeyTable.teamId
    var app by AppEntity optionalReferencedOn ApiKeyTable.appId
    var appId by ApiKeyTable.appId
    val rateLimits by RateLimitEntity referrersOn RateLimitTable.apiKeyId
}
