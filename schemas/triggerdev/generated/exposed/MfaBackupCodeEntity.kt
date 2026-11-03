package triggerdev.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class MfaBackupCodeEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, MfaBackupCodeEntity>(MfaBackupCodeTable)

    var code by MfaBackupCodeTable.code
    var user by UserEntity referencedOn MfaBackupCodeTable.userId
    var userId by MfaBackupCodeTable.userId
    var usedAt by MfaBackupCodeTable.usedAt
    var createdAt by MfaBackupCodeTable.createdAt
    var updatedAt by MfaBackupCodeTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(MfaBackupCodeTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
