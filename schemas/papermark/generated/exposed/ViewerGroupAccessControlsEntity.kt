package papermark.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class ViewerGroupAccessControlsEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ViewerGroupAccessControlsEntity>(ViewerGroupAccessControlsTable)

    var groupId by ViewerGroupAccessControlsTable.groupId
    var group by ViewerGroupEntity referencedOn ViewerGroupAccessControlsTable.groupId
    var itemId by ViewerGroupAccessControlsTable.itemId
    var itemType by ViewerGroupAccessControlsTable.itemType
    var canView by ViewerGroupAccessControlsTable.canView
    var canDownload by ViewerGroupAccessControlsTable.canDownload
    var createdAt by ViewerGroupAccessControlsTable.createdAt
    var updatedAt by ViewerGroupAccessControlsTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ViewerGroupAccessControlsTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
