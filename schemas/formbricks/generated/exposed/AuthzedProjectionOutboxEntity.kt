package formbricks.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class AuthzedProjectionOutboxEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, AuthzedProjectionOutboxEntity>(AuthzedProjectionOutboxTable)

    var targetType by AuthzedProjectionOutboxTable.targetType
    var primaryId by AuthzedProjectionOutboxTable.primaryId
    var secondaryId by AuthzedProjectionOutboxTable.secondaryId
    var isRevocation by AuthzedProjectionOutboxTable.isRevocation
    var attempts by AuthzedProjectionOutboxTable.attempts
    var permanentFailures by AuthzedProjectionOutboxTable.permanentFailures
    var availableAt by AuthzedProjectionOutboxTable.availableAt
    var leasedAt by AuthzedProjectionOutboxTable.leasedAt
    var leaseExpiresAt by AuthzedProjectionOutboxTable.leaseExpiresAt
    var leaseOwner by AuthzedProjectionOutboxTable.leaseOwner
    var processedAt by AuthzedProjectionOutboxTable.processedAt
    var deadLetteredAt by AuthzedProjectionOutboxTable.deadLetteredAt
    var lastAttemptAt by AuthzedProjectionOutboxTable.lastAttemptAt
    var lastErrorCode by AuthzedProjectionOutboxTable.lastErrorCode
    var createdAt by AuthzedProjectionOutboxTable.createdAt
    var updatedAt by AuthzedProjectionOutboxTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(AuthzedProjectionOutboxTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
