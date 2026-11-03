package langfuse.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class PendingDeletionEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, PendingDeletionEntity>(PendingDeletionTable)

    var projectId by PendingDeletionTable.projectId
    var project by ProjectEntity referencedOn PendingDeletionTable.projectId
    var `object` by PendingDeletionTable.`object`
    var objectId by PendingDeletionTable.objectId
    var isDeleted by PendingDeletionTable.isDeleted
    var createdAt by PendingDeletionTable.createdAt
    var updatedAt by PendingDeletionTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(PendingDeletionTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
