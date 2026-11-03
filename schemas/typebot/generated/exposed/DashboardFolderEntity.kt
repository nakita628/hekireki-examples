package typebot.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class DashboardFolderEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, DashboardFolderEntity>(DashboardFolderTable)

    var createdAt by DashboardFolderTable.createdAt
    var updatedAt by DashboardFolderTable.updatedAt
    var name by DashboardFolderTable.name
    var parentFolderId by DashboardFolderTable.parentFolderId
    var workspaceId by DashboardFolderTable.workspaceId
    var parentFolder by DashboardFolderEntity optionalReferencedOn DashboardFolderTable.parentFolderId
    var workspace by WorkspaceEntity referencedOn DashboardFolderTable.workspaceId
    val childrenFolder by DashboardFolderEntity optionalReferrersOn DashboardFolderTable.parentFolderId
    val typebots by TypebotEntity optionalReferrersOn TypebotTable.folderId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(DashboardFolderTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
