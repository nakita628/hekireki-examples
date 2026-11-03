package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class AccessCodeEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, AccessCodeEntity>(AccessCodeTable)

    var code by AccessCodeTable.code
    var clientId by AccessCodeTable.clientId
    var client by OAuthClientEntity optionalReferencedOn AccessCodeTable.clientId
    var expiresAt by AccessCodeTable.expiresAt
    var scopes by AccessCodeTable.scopes
    var userId by AccessCodeTable.userId
    var user by UserEntity optionalReferencedOn AccessCodeTable.userId
    var teamId by AccessCodeTable.teamId
    var team by TeamEntity optionalReferencedOn AccessCodeTable.teamId
    var codeChallenge by AccessCodeTable.codeChallenge
    var codeChallengeMethod by AccessCodeTable.codeChallengeMethod
}
