package formbricks.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class DeletionCleanupEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, DeletionCleanupEntity>(DeletionCleanupTable)

    var createdAt by DeletionCleanupTable.createdAt
    var updatedAt by DeletionCleanupTable.updatedAt
    var kind by DeletionCleanupTable.kind
    var organizationId by DeletionCleanupTable.organizationId
    var workspaceId by DeletionCleanupTable.workspaceId
    var surveyId by DeletionCleanupTable.surveyId
    var tenantIds by DeletionCleanupTable.tenantIds
    var responseIds by DeletionCleanupTable.responseIds
    var fileKeys by DeletionCleanupTable.fileKeys
    var attempts by DeletionCleanupTable.attempts
    var nextAttemptAt by DeletionCleanupTable.nextAttemptAt
    var lastError by DeletionCleanupTable.lastError

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(DeletionCleanupTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
