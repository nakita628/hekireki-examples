package papermark.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class PermissionGroupAccessControlsEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, PermissionGroupAccessControlsEntity>(PermissionGroupAccessControlsTable)

    var groupId by PermissionGroupAccessControlsTable.groupId
    var group by PermissionGroupEntity referencedOn PermissionGroupAccessControlsTable.groupId
    var itemId by PermissionGroupAccessControlsTable.itemId
    var itemType by PermissionGroupAccessControlsTable.itemType
    var canView by PermissionGroupAccessControlsTable.canView
    var canDownload by PermissionGroupAccessControlsTable.canDownload
    var canDownloadOriginal by PermissionGroupAccessControlsTable.canDownloadOriginal
    var createdAt by PermissionGroupAccessControlsTable.createdAt
    var updatedAt by PermissionGroupAccessControlsTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(PermissionGroupAccessControlsTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
