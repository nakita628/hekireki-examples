package inboxzero.models

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

    var password by AccountTable.password
    var createdAt by AccountTable.createdAt
    var updatedAt by AccountTable.updatedAt
    var userId by AccountTable.userId
    var provider by AccountTable.provider
    var type by AccountTable.type
    var providerAccountId by AccountTable.providerAccountId
    var refreshToken by AccountTable.refreshToken
    var refreshTokenExpiresAt by AccountTable.refreshTokenExpiresAt
    var accessToken by AccountTable.accessToken
    var expiresAt by AccountTable.expiresAt
    var disconnectedAt by AccountTable.disconnectedAt
    var tokenType by AccountTable.tokenType
    var scope by AccountTable.scope
    var idToken by AccountTable.idToken
    var sessionState by AccountTable.sessionState
    var user by UserEntity referencedOn AccountTable.userId
    val emailAccount by EmailAccountEntity optionalBackReferencedOn EmailAccountTable.accountId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(AccountTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
