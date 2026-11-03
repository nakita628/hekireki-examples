package typebot.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class AccountEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, AccountEntity>(AccountTable)

    var userId by AccountTable.userId
    var type by AccountTable.type
    var provider by AccountTable.provider
    var providerAccountId by AccountTable.providerAccountId
    var refreshToken by AccountTable.refreshToken
    var accessToken by AccountTable.accessToken
    var expiresAt by AccountTable.expiresAt
    var tokenType by AccountTable.tokenType
    var scope by AccountTable.scope
    var idToken by AccountTable.idToken
    var sessionState by AccountTable.sessionState
    var oauthTokenSecret by AccountTable.oauthTokenSecret
    var oauthToken by AccountTable.oauthToken
    var refreshTokenExpiresIn by AccountTable.refreshTokenExpiresIn
    var user by UserEntity referencedOn AccountTable.userId
}
