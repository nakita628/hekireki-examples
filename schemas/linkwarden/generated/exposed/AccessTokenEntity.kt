package linkwarden.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class AccessTokenEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, AccessTokenEntity>(AccessTokenTable)

    var name by AccessTokenTable.name
    var user by UserEntity referencedOn AccessTokenTable.userId
    var userId by AccessTokenTable.userId
    var token by AccessTokenTable.token
    var revoked by AccessTokenTable.revoked
    var isSession by AccessTokenTable.isSession
    var expires by AccessTokenTable.expires
    var lastUsedAt by AccessTokenTable.lastUsedAt
    var createdAt by AccessTokenTable.createdAt
    var updatedAt by AccessTokenTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(AccessTokenTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
