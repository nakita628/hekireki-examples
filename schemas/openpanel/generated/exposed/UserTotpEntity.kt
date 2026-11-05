package openpanel.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class UserTotpEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, UserTotpEntity>(UserTotpTable)

    var userId by UserTotpTable.userId
    var user by UserEntity referencedOn UserTotpTable.userId
    var secret by UserTotpTable.secret
    var recoveryCodes by UserTotpTable.recoveryCodes
    var enabledAt by UserTotpTable.enabledAt
    var createdAt by UserTotpTable.createdAt
    var updatedAt by UserTotpTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(UserTotpTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
