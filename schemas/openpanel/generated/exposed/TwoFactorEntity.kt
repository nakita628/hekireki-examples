package openpanel.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class TwoFactorEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, TwoFactorEntity>(TwoFactorTable)

    var userId by TwoFactorTable.userId
    var user by UserEntity referencedOn TwoFactorTable.userId
    var secret by TwoFactorTable.secret
    var backupCodes by TwoFactorTable.backupCodes
    var verified by TwoFactorTable.verified
    var failedVerificationCount by TwoFactorTable.failedVerificationCount
    var lockedUntil by TwoFactorTable.lockedUntil
    var createdAt by TwoFactorTable.createdAt
    var updatedAt by TwoFactorTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(TwoFactorTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
