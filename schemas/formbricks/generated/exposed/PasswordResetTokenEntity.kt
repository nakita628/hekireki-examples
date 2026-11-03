package formbricks.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class PasswordResetTokenEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, PasswordResetTokenEntity>(PasswordResetTokenTable)

    var createdAt by PasswordResetTokenTable.createdAt
    var updatedAt by PasswordResetTokenTable.updatedAt
    var tokenHash by PasswordResetTokenTable.tokenHash
    var expiresAt by PasswordResetTokenTable.expiresAt
    var user by UserEntity referencedOn PasswordResetTokenTable.userId
    var userId by PasswordResetTokenTable.userId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(PasswordResetTokenTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
