package papermark.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class TaskAssignmentEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, TaskAssignmentEntity>(TaskAssignmentTable)

    var taskId by TaskAssignmentTable.taskId
    var task by TaskEntity referencedOn TaskAssignmentTable.taskId
    var viewerId by TaskAssignmentTable.viewerId
    var viewer by ViewerEntity optionalReferencedOn TaskAssignmentTable.viewerId
    var groupId by TaskAssignmentTable.groupId
    var group by ViewerGroupEntity optionalReferencedOn TaskAssignmentTable.groupId
    var linkId by TaskAssignmentTable.linkId
    var link by LinkEntity optionalReferencedOn TaskAssignmentTable.linkId
    var email by TaskAssignmentTable.email
    var notifiedAt by TaskAssignmentTable.notifiedAt
    var lastReminderAt by TaskAssignmentTable.lastReminderAt
    var createdAt by TaskAssignmentTable.createdAt
    var updatedAt by TaskAssignmentTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(TaskAssignmentTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
