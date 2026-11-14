package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class AccountCredentialEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, AccountCredentialEntity>(AccountCredentialTable)

    var userId by AccountCredentialTable.userId
    var provider by AccountCredentialTable.provider
    var password by AccountCredentialTable.password
    var accessToken by AccountCredentialTable.accessToken
    var refreshToken by AccountCredentialTable.refreshToken
    var idToken by AccountCredentialTable.idToken
    var accessTokenExpiresAt by AccountCredentialTable.accessTokenExpiresAt
    var refreshTokenExpiresAt by AccountCredentialTable.refreshTokenExpiresAt
    var scope by AccountCredentialTable.scope
    var createdAt by AccountCredentialTable.createdAt
    var updatedAt by AccountCredentialTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(AccountCredentialTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
