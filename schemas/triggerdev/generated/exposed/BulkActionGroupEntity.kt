package triggerdev.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class BulkActionGroupEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, BulkActionGroupEntity>(BulkActionGroupTable)

    var friendlyId by BulkActionGroupTable.friendlyId
    var project by ProjectEntity referencedOn BulkActionGroupTable.projectId
    var projectId by BulkActionGroupTable.projectId
    var environment by RuntimeEnvironmentEntity optionalReferencedOn BulkActionGroupTable.environmentId
    var environmentId by BulkActionGroupTable.environmentId
    var type by BulkActionGroupTable.type
    var status by BulkActionGroupTable.status
    var queryName by BulkActionGroupTable.queryName
    var params by BulkActionGroupTable.params
    var dedupeKey by BulkActionGroupTable.dedupeKey
    var cursor by BulkActionGroupTable.cursor
    var successCount by BulkActionGroupTable.successCount
    var failureCount by BulkActionGroupTable.failureCount
    var totalCount by BulkActionGroupTable.totalCount
    var completionNotification by BulkActionGroupTable.completionNotification
    var user by UserEntity optionalReferencedOn BulkActionGroupTable.userId
    var userId by BulkActionGroupTable.userId
    var name by BulkActionGroupTable.name
    var completedAt by BulkActionGroupTable.completedAt
    var createdAt by BulkActionGroupTable.createdAt
    var updatedAt by BulkActionGroupTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(BulkActionGroupTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
