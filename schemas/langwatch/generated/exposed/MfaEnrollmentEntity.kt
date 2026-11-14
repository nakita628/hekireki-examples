package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class MfaEnrollmentEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, MfaEnrollmentEntity>(MfaEnrollmentTable)

    var enrollmentId by MfaEnrollmentTable.enrollmentId
    var method by MfaEnrollmentTable.method
    var state by MfaEnrollmentTable.state
    var enrolledAt by MfaEnrollmentTable.enrolledAt
    var confirmedAt by MfaEnrollmentTable.confirmedAt
    var expiredAt by MfaEnrollmentTable.expiredAt
    var disabledAt by MfaEnrollmentTable.disabledAt
    var disabledVia by MfaEnrollmentTable.disabledVia
    var backupCodeCount by MfaEnrollmentTable.backupCodeCount
    var consumedBackupCodeIndexes by MfaEnrollmentTable.consumedBackupCodeIndexes
    var failedCount by MfaEnrollmentTable.failedCount
    var occurredAt by MfaEnrollmentTable.occurredAt
    var lastEventId by MfaEnrollmentTable.lastEventId
    var acceptedAt by MfaEnrollmentTable.acceptedAt
    var projectionVersion by MfaEnrollmentTable.projectionVersion
    var createdAt by MfaEnrollmentTable.createdAt
    var updatedAt by MfaEnrollmentTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(MfaEnrollmentTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
