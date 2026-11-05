package openpanel.models

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

    var userId by AccountTable.userId
    var user by UserEntity referencedOn AccountTable.userId
    var email by AccountTable.email
    var provider by AccountTable.provider
    var providerId by AccountTable.providerId
    var accountId by AccountTable.accountId
    var idToken by AccountTable.idToken
    var accessToken by AccountTable.accessToken
    var refreshToken by AccountTable.refreshToken
    var accessTokenExpiresAt by AccountTable.accessTokenExpiresAt
    var refreshTokenExpiresAt by AccountTable.refreshTokenExpiresAt
    var scope by AccountTable.scope
    var password by AccountTable.password
    var createdAt by AccountTable.createdAt
    var updatedAt by AccountTable.updatedAt
    val resetPasswords by ResetPasswordEntity referrersOn ResetPasswordTable.accountId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(AccountTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
