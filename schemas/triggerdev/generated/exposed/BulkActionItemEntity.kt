package triggerdev.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class BulkActionItemEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, BulkActionItemEntity>(BulkActionItemTable)

    var friendlyId by BulkActionItemTable.friendlyId
    var groupId by BulkActionItemTable.groupId
    var type by BulkActionItemTable.type
    var status by BulkActionItemTable.status
    var sourceRun by TaskRunEntity referencedOn BulkActionItemTable.sourceRunId
    var sourceRunId by BulkActionItemTable.sourceRunId
    var destinationRun by TaskRunEntity optionalReferencedOn BulkActionItemTable.destinationRunId
    var destinationRunId by BulkActionItemTable.destinationRunId
    var error by BulkActionItemTable.error
    var createdAt by BulkActionItemTable.createdAt
    var updatedAt by BulkActionItemTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(BulkActionItemTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
