package triggerdev.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class PersonalAccessTokenEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, PersonalAccessTokenEntity>(PersonalAccessTokenTable)

    var name by PersonalAccessTokenTable.name
    var encryptedToken by PersonalAccessTokenTable.encryptedToken
    var obfuscatedToken by PersonalAccessTokenTable.obfuscatedToken
    var hashedToken by PersonalAccessTokenTable.hashedToken
    var user by UserEntity referencedOn PersonalAccessTokenTable.userId
    var userId by PersonalAccessTokenTable.userId
    var revokedAt by PersonalAccessTokenTable.revokedAt
    var lastAccessedAt by PersonalAccessTokenTable.lastAccessedAt
    var expiresAt by PersonalAccessTokenTable.expiresAt
    var idleTimeoutSeconds by PersonalAccessTokenTable.idleTimeoutSeconds
    var expiryReminderSentAt by PersonalAccessTokenTable.expiryReminderSentAt
    var expiredNoticeSentAt by PersonalAccessTokenTable.expiredNoticeSentAt
    var createdAt by PersonalAccessTokenTable.createdAt
    var updatedAt by PersonalAccessTokenTable.updatedAt
    val authorizationCodes by AuthorizationCodeEntity optionalReferrersOn AuthorizationCodeTable.personalAccessTokenId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(PersonalAccessTokenTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
