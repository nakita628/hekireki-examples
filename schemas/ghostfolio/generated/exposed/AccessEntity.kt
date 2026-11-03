package ghostfolio.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class AccessEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, AccessEntity>(AccessTable)

    var alias by AccessTable.alias
    var createdAt by AccessTable.createdAt
    var expiresAt by AccessTable.expiresAt
    var granteeUser by UserEntity optionalReferencedOn AccessTable.granteeUserId
    var granteeUserId by AccessTable.granteeUserId
    var lastUsedAt by AccessTable.lastUsedAt
    var scopes by AccessTable.scopes
    var settings by AccessTable.settings
    var type by AccessTable.type
    var updatedAt by AccessTable.updatedAt
    var userId by AccessTable.userId
    var user by UserEntity referencedOn AccessTable.userId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(AccessTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
