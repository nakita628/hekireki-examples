package documenso.models

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
    var userId by FolderTable.userId
    var user by UserEntity referencedOn FolderTable.userId
    var teamId by FolderTable.teamId
    var team by TeamEntity referencedOn FolderTable.teamId
    var pinned by FolderTable.pinned
    var parentId by FolderTable.parentId
    var parent by FolderEntity optionalReferencedOn FolderTable.parentId
    val envelopes by EnvelopeEntity optionalReferrersOn EnvelopeTable.folderId
    var createdAt by FolderTable.createdAt
    var updatedAt by FolderTable.updatedAt
    val subfolders by FolderEntity optionalReferrersOn FolderTable.parentId
    var visibility by FolderTable.visibility
    var type by FolderTable.type

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(FolderTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
