package hoppscotch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class AccountEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, AccountEntity>(AccountTable)

    var userId by AccountTable.userId
    var provider by AccountTable.provider
    var providerAccountId by AccountTable.providerAccountId
    var providerRefreshToken by AccountTable.providerRefreshToken
    var providerAccessToken by AccountTable.providerAccessToken
    var providerScope by AccountTable.providerScope
    var loggedIn by AccountTable.loggedIn
    var user by UserEntity referencedOn AccountTable.userId
}
