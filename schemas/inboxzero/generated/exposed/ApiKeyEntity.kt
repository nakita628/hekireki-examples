package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class ApiKeyEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ApiKeyEntity>(ApiKeyTable)

    var createdAt by ApiKeyTable.createdAt
    var updatedAt by ApiKeyTable.updatedAt
    var name by ApiKeyTable.name
    var hashedKey by ApiKeyTable.hashedKey
    var isActive by ApiKeyTable.isActive
    var expiresAt by ApiKeyTable.expiresAt
    var lastUsedAt by ApiKeyTable.lastUsedAt
    var scopes by ApiKeyTable.scopes
    var userId by ApiKeyTable.userId
    var user by UserEntity referencedOn ApiKeyTable.userId
    var emailAccountId by ApiKeyTable.emailAccountId
    var emailAccount by EmailAccountEntity optionalReferencedOn ApiKeyTable.emailAccountId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ApiKeyTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
