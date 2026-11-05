package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class FilingFolderEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, FilingFolderEntity>(FilingFolderTable)

    var createdAt by FilingFolderTable.createdAt
    var updatedAt by FilingFolderTable.updatedAt
    var folderId by FilingFolderTable.folderId
    var folderName by FilingFolderTable.folderName
    var folderPath by FilingFolderTable.folderPath
    var driveConnectionId by FilingFolderTable.driveConnectionId
    var driveConnection by DriveConnectionEntity referencedOn FilingFolderTable.driveConnectionId
    var emailAccountId by FilingFolderTable.emailAccountId
    var emailAccount by EmailAccountEntity referencedOn FilingFolderTable.emailAccountId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(FilingFolderTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
