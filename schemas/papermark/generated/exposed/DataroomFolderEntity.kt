package papermark.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class DataroomFolderEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, DataroomFolderEntity>(DataroomFolderTable)

    var name by DataroomFolderTable.name
    var path by DataroomFolderTable.path
    var parentId by DataroomFolderTable.parentId
    var icon by DataroomFolderTable.icon
    var color by DataroomFolderTable.color
    val documents by DataroomDocumentEntity optionalReferrersOn DataroomDocumentTable.folderId
    val childFolders by DataroomFolderEntity optionalReferrersOn DataroomFolderTable.parentId
    var parentFolder by DataroomFolderEntity optionalReferencedOn DataroomFolderTable.parentId
    val uploadTasks by TaskEntity optionalReferrersOn TaskTable.uploadFolderId
    var dataroomId by DataroomFolderTable.dataroomId
    var dataroom by DataroomEntity referencedOn DataroomFolderTable.dataroomId
    var orderIndex by DataroomFolderTable.orderIndex
    var hierarchicalIndex by DataroomFolderTable.hierarchicalIndex
    var createdAt by DataroomFolderTable.createdAt
    var updatedAt by DataroomFolderTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(DataroomFolderTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
