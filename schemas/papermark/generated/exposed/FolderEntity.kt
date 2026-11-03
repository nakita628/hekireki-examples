package papermark.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class FolderEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, FolderEntity>(FolderTable)

    var name by FolderTable.name
    var path by FolderTable.path
    var parentId by FolderTable.parentId
    var hiddenInAllDocuments by FolderTable.hiddenInAllDocuments
    var icon by FolderTable.icon
    var color by FolderTable.color
    val documents by DocumentEntity optionalReferrersOn DocumentTable.folderId
    val childFolders by FolderEntity optionalReferrersOn FolderTable.parentId
    var parentFolder by FolderEntity optionalReferencedOn FolderTable.parentId
    var teamId by FolderTable.teamId
    var team by TeamEntity referencedOn FolderTable.teamId
    var createdAt by FolderTable.createdAt
    var updatedAt by FolderTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(FolderTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
