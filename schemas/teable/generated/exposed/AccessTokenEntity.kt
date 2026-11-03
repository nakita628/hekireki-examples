package teable.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class AccessTokenEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, AccessTokenEntity>(AccessTokenTable)

    var name by AccessTokenTable.name
    var description by AccessTokenTable.description
    var userId by AccessTokenTable.userId
    var scopes by AccessTokenTable.scopes
    var spaceIds by AccessTokenTable.spaceIds
    var baseIds by AccessTokenTable.baseIds
    var sign by AccessTokenTable.sign
    var clientId by AccessTokenTable.clientId
    var hasFullAccess by AccessTokenTable.hasFullAccess
    var expiredTime by AccessTokenTable.expiredTime
    var lastUsedTime by AccessTokenTable.lastUsedTime
    var createdTime by AccessTokenTable.createdTime
    var lastModifiedTime by AccessTokenTable.lastModifiedTime

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(AccessTokenTable.lastModifiedTime)) {
                lastModifiedTime = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
