package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class SignInAttemptLockEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, SignInAttemptLockEntity>(SignInAttemptLockTable)

    var identifierHash by SignInAttemptLockTable.identifierHash
    var failedCount by SignInAttemptLockTable.failedCount
    var lockedUntil by SignInAttemptLockTable.lockedUntil
    var consecutiveLockouts by SignInAttemptLockTable.consecutiveLockouts
    var heldForReview by SignInAttemptLockTable.heldForReview
    var userId by SignInAttemptLockTable.userId
    var createdAt by SignInAttemptLockTable.createdAt
    var updatedAt by SignInAttemptLockTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(SignInAttemptLockTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
