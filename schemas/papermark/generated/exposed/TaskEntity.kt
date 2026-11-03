package papermark.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class TaskEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, TaskEntity>(TaskTable)

    var title by TaskTable.title
    var description by TaskTable.description
    var type by TaskTable.type
    var status by TaskTable.status
    var category by TaskTable.category
    var dueDate by TaskTable.dueDate
    var orderIndex by TaskTable.orderIndex
    var taskListId by TaskTable.taskListId
    var taskList by TaskListEntity referencedOn TaskTable.taskListId
    var dataroomId by TaskTable.dataroomId
    var dataroom by DataroomEntity referencedOn TaskTable.dataroomId
    var teamId by TaskTable.teamId
    var team by TeamEntity referencedOn TaskTable.teamId
    var uploadFolderId by TaskTable.uploadFolderId
    var uploadFolder by DataroomFolderEntity optionalReferencedOn TaskTable.uploadFolderId
    var createdByUserId by TaskTable.createdByUserId
    var createdByUser by UserEntity optionalReferencedOn TaskTable.createdByUserId
    val assignments by TaskAssignmentEntity referrersOn TaskAssignmentTable.taskId
    val activities by TaskActivityEntity referrersOn TaskActivityTable.taskId
    val uploads by DocumentUploadEntity optionalReferrersOn DocumentUploadTable.taskId
    var lastActivityNotifiedAt by TaskTable.lastActivityNotifiedAt
    var createdAt by TaskTable.createdAt
    var updatedAt by TaskTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(TaskTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
