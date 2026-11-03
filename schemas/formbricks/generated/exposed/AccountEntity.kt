package formbricks.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class AccountEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, AccountEntity>(AccountTable)

    var createdAt by AccountTable.createdAt
    var updatedAt by AccountTable.updatedAt
    var user by UserEntity referencedOn AccountTable.userId
    var userId by AccountTable.userId
    var type by AccountTable.type
    var provider by AccountTable.provider
    var providerAccountId by AccountTable.providerAccountId
    var accessToken by AccountTable.accessToken
    var refreshToken by AccountTable.refreshToken
    var expiresAt by AccountTable.expiresAt
    var extExpiresIn by AccountTable.extExpiresIn
    var tokenType by AccountTable.tokenType
    var scope by AccountTable.scope
    var idToken by AccountTable.idToken
    var sessionState by AccountTable.sessionState
    var password by AccountTable.password
    var accessTokenExpiresAt by AccountTable.accessTokenExpiresAt
    var refreshTokenExpiresAt by AccountTable.refreshTokenExpiresAt
    var issuer by AccountTable.issuer

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(AccountTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
