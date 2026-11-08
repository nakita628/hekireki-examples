package workoutcool.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class AccountEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, AccountEntity>(AccountTable)

    var accountId by AccountTable.accountId
    var providerId by AccountTable.providerId
    var userId by AccountTable.userId
    var user by UserEntity referencedOn AccountTable.userId
    var accessToken by AccountTable.accessToken
    var refreshToken by AccountTable.refreshToken
    var idToken by AccountTable.idToken
    var accessTokenExpiresAt by AccountTable.accessTokenExpiresAt
    var refreshTokenExpiresAt by AccountTable.refreshTokenExpiresAt
    var scope by AccountTable.scope
    var password by AccountTable.password
    var createdAt by AccountTable.createdAt
    var updatedAt by AccountTable.updatedAt
}
